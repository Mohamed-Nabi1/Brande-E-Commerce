using E_Commerce.DAL;
using System.Data;

namespace E_Commerce.BL
{
    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region AddToCart
        public void AddToCart(AddToCartDto addToCartDto, string CustomerId)
        {
            Cart? cart = _unitOfWork.CartRepo.GetCartProductByCustomerId(new Guid(CustomerId));
            if (cart == null)
            {
                cart = new Cart();
                cart.CartId = Guid.NewGuid();
                cart.CustomerId = new Guid(CustomerId);
                cart.Products = new List<CartProduct>
                    {
                        new CartProduct
                        {
                            CartId = cart.CartId,
                            ProductId = addToCartDto.ProductId,
                            ProductCount = addToCartDto.ProductCount,
                            Color = (Color)Enum.Parse<Color>(addToCartDto.Color),
                            Size = (Size)Enum.Parse<Size>(addToCartDto.Size)
                        }
                    };
                _unitOfWork.CartRepo.Add(cart);
            }
  
            else
            {
                var productCheck = cart.Products.FirstOrDefault(c =>
                    c.ProductId == addToCartDto.ProductId &&
                    c.Color == (Color)Enum.Parse<Color>(addToCartDto.Color) &&
                    c.Size == (Size)Enum.Parse<Size>(addToCartDto.Size));

                if (cart.Products == null || productCheck == null)
                {
                    var cartProduct = new CartProduct
                    {
                        ProductId = addToCartDto.ProductId,
                        CartId = cart.CartId,
                        ProductCount = addToCartDto.ProductCount,
                        Color = (Color)Enum.Parse<Color>(addToCartDto.Color),
                        Size = (Size)Enum.Parse<Size>(addToCartDto.Size)
                    };

                    cart.Products.Add(cartProduct);
                }
                else
                {
                    int availableQty = _unitOfWork.ProductsRepo.GetProductDetails(addToCartDto.ProductId)
                        .Product_Color_Size_Quantity
                        .FirstOrDefault(p => p.Color == (Color)Enum.Parse<Color>(addToCartDto.Color) &&
                                             p.Size == (Size)Enum.Parse<Size>(addToCartDto.Size))?.Quantity ?? 0;

                    int newQuantity = productCheck.ProductCount + addToCartDto.ProductCount;

                    productCheck.ProductCount = newQuantity <= availableQty ? newQuantity : availableQty;
                }
            }

            _unitOfWork.SaveChange();
        }
        #endregion



        //public bool ClearCartProducts(Guid CustomerId)
        //{
        //    var cart =  _unitOfWork.CartRepo.GetCartProductByCustomerId(CustomerId);
        //    if (cart is null) 
        //        return false;
        //    cart.Products.Clear();
        //    _unitOfWork.SaveChange();
        //    return true;
        //}


        #region GetAllCartProducts
        public GetCartProductByCustomerIdDto GetCartProductsByCustomerId(Guid customerId)
        {
            Cart cart = _unitOfWork.CartRepo.GetCartProductByCustomerId(customerId);
            decimal total = 0;
            string strImge = "";
            int qty = 0;

            if (cart == null)
            {
                return new GetCartProductByCustomerIdDto
                {
                    CartId = Guid.NewGuid(),
                    Products = new List<ProductDto>()
                };
            }
            else
            {

                List<ProductDto> products = new List<ProductDto>();
                foreach (var cp in cart.Products)
                {
                    if (_unitOfWork.ProductsRepo.GetProductDetails(cp.ProductId).ProductImages.Count==0)
                    {
                        strImge = "null";
                    }
                    else
                    {
                        strImge = _unitOfWork.ProductsRepo.GetProductDetails(cp.ProductId).ProductImages[0].ImageURL;
                    }

                    
                        products.Add(new ProductDto
                        {
                            ProductId = cp.ProductId,
                            Name = cp.Product.Name,
                            Description = cp.Product.Description,
                            Price = cp.Product.Price,
                            Image = strImge,
                            QuantityInStock = cp.Product.Product_Color_Size_Quantity
                            .FirstOrDefault(p => p.Color == cp.Color && p.Size == cp.Size).Quantity,
                            Quantity = cp.ProductCount,
                            Discount = cp.Product.Discount,
                            Color = cp.Color.ToString(),
                            Size = cp.Size.ToString()
                        });
                    
                    //int quantityInStock = info.Quantity;
                    //int userEnteredQuantity = cp.ProductCount;
                    
                    total += cp.Product.Price*cp.ProductCount;
                    
                }
                    GetCartProductByCustomerIdDto cartDto = new GetCartProductByCustomerIdDto
                    {
                        CartId = cart.CartId,
                        Products = products,
                        TotalCost = total
                    };

            return cartDto;
            }

            

        }
        #endregion

        #region UpdateCartProduct Increase Count
        public bool UpdateCartProduct(Guid customerId, AddToCartDto cartproduct)
        {
            Cart? cart = _unitOfWork.CartRepo.GetCartProductByCustomerId(customerId);

            if (cart != null && cart.Products.FirstOrDefault(p=>p.ProductId == cartproduct.ProductId 
                && cartproduct.Color==p.Color.ToString()&&cartproduct.Size==p.Size.ToString())!=null)
            {
                
                int count = cart.Products.FirstOrDefault(p => p.ProductId == cartproduct.ProductId
                            && cartproduct.Color == p.Color.ToString() && cartproduct.Size == p.Size.ToString()).ProductCount + 1;

                if (count <= _unitOfWork.ProductsRepo.GetProductDetails(cartproduct.ProductId)
                    .Product_Color_Size_Quantity.FirstOrDefault(p => p.ProductID == cartproduct.ProductId
                            && cartproduct.Color == p.Color.ToString() && cartproduct.Size == p.Size.ToString()).Quantity)
                {
                    cart.Products.FirstOrDefault(p => p.ProductId == cartproduct.ProductId
                            && cartproduct.Color == p.Color.ToString() && cartproduct.Size == p.Size.ToString()).ProductCount += 1;
                    _unitOfWork.SaveChange();

                }
                return true;
                
            }
            return false;
        }
        #endregion 

        #region UpdateCartProduct Decrease Count
        public bool UpdateCartProductDecr(Guid customerId, AddToCartDto cartproduct)
        {
            Cart? cart = _unitOfWork.CartRepo.GetCartProductByCustomerId(customerId);

            if (cart != null && cart.Products.FirstOrDefault(p => p.ProductId == cartproduct.ProductId
                && cartproduct.Color == p.Color.ToString() && cartproduct.Size == p.Size.ToString()) != null)
            {

                int count = cart.Products.FirstOrDefault(p => p.ProductId == cartproduct.ProductId
                            && cartproduct.Color == p.Color.ToString() && cartproduct.Size == p.Size.ToString()).ProductCount - 1;

                if (count > 0 )
                {
                    cart.Products.FirstOrDefault(p => p.ProductId == cartproduct.ProductId
                            && cartproduct.Color == p.Color.ToString() && cartproduct.Size == p.Size.ToString()).ProductCount -= 1;
                    _unitOfWork.SaveChange();

                }
                return true;

            }
            return false;
        }
        #endregion

        #region Delete Cart Products
        public bool DeleteCart(Guid customerId)
        {
            Cart cart = _unitOfWork.CartRepo.GetCartProductByCustomerId(customerId);
            _unitOfWork.CartRepo.Delete(cart);
            if (cart == null)
            {
                return false;
            }

            _unitOfWork.CartRepo.Delete(cart);
            return _unitOfWork.SaveChange() > 0;

        }
        #endregion

        #region DeleteCartProduct
        public bool DeleteCartProduct(DeleteCardProductDto deleteCardProductDto)
        {
            Cart? cart = _unitOfWork.CartRepo.GetById(deleteCardProductDto.CartId);
            CartProduct cart2 = _unitOfWork.CartProductRepo.GetById(deleteCardProductDto.ProductId, deleteCardProductDto.CartId);

            if (cart == null || cart2 == null)
            {

                return false;
            }
            else
            {
                _unitOfWork.CartProductRepo.Delete(_unitOfWork.CartProductRepo.GetAll()
                    .FirstOrDefault(c => c.ProductId == deleteCardProductDto.ProductId
                    && c.CartId == deleteCardProductDto.CartId
                    && c.Color == (Color)Enum.Parse<Color>(deleteCardProductDto.Color)
                    && c.Size == (Size)Enum.Parse<Size>(deleteCardProductDto.Size)));
                _unitOfWork.SaveChange();
            }

            return true;
        }

        

        #endregion

    }

}