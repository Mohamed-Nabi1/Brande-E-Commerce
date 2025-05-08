
namespace E_Commerce.BL
{
    public interface ICartManager
    {
        GetCartProductByCustomerIdDto GetCartProductsByCustomerId(Guid customerId);

        void AddToCart(AddToCartDto addToCartDto,string customerId);
        bool UpdateCartProduct(Guid CustomerId, AddToCartDto CartProduct);
        bool UpdateCartProductDecr(Guid CustomerId, AddToCartDto CartProduct);
        bool DeleteCartProduct(DeleteCardProductDto deleteCardProductDto);
        bool DeleteCart(Guid customerId);



    }
}
