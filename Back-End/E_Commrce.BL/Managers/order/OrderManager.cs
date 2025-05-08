using E_Commerce.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_Commerce.BL;

public class OrderManager : IOrderManager
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<OrderReadDto> GetAllOrders()
    {
        List<Order> orders = _unitOfWork.OrderRepo.GetAll();
        if (orders is null) { return null!; }
        return orders.Select(o => new OrderReadDto
        {
            Id = o.Id,
            OrderData = o.OrderData,
            PaymentStatus = o.PaymentStatus.ToString(),
            PaymentMethod = o.PaymentMethod.ToString(),
            OrderStatus = o.OrderStatus.ToString(),
            Discount = o.Discount,
            ArrivalDate = o.ArrivalDate,
            Street = o.Street,
            City = o.City,
            Country = o.Country.ToString(),
            TotalPrice = o.TotalPrice,
        }).ToList();
    }

    public List<OrderReadDto> GetAllOrderswithCustName()
    {
        List<Order> orders = _unitOfWork.OrderRepo.GetOrdersWithCustomer();
        if (orders is null) { return null!; }
        return orders.Select(o => new OrderReadDto
        {
            Id = o.Id,
            OrderData = o.OrderData,
            PaymentStatus = o.PaymentStatus.ToString(),
            PaymentMethod = o.PaymentMethod.ToString(),
            OrderStatus = o.OrderStatus.ToString(),
            Discount = o.Discount,
            ArrivalDate = o.ArrivalDate,
            Street = o.Street,
            City = o.City,
            Country = o.Country.ToString(),
            CustomerName = o.Customer.FirstName + ' ' + o.Customer.LastName,
            TotalPrice = o.TotalPrice,
        }).ToList();
    }

    public OrderReadDto GetOrderById(Guid id)
    {
        Order? order = _unitOfWork.OrderRepo.GetById(id);
        if (order is null) { return null!; }
        return new OrderReadDto
        {
            OrderData = order.OrderData,
            PaymentStatus = order.PaymentStatus.ToString(),
            PaymentMethod = order.PaymentMethod.ToString(),
            OrderStatus = order.OrderStatus.ToString(),
            Discount = order.Discount,
            ArrivalDate = order.ArrivalDate,
            Street = order.Street,
            City = order.City,
            Country = order.Country.ToString(),
            TotalPrice = order.TotalPrice,
        };
    }

    public bool AddOrder(OrderAddDto orderAdd, Guid customerId)
    {
        bool checkQuantity = CheckQuantityOfProductsBeforeOrder(customerId);
        if (!checkQuantity) { return false; }


        Order order = new()
        {
            Id = Guid.NewGuid(),
            OrderData = orderAdd.OrderData,
            PaymentStatus = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), orderAdd.PaymentStatus),
            PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), orderAdd.PaymentMethod),
            OrderStatus = orderAdd.OrderStatus,
            Discount = (double)orderAdd.Discount,
            ArrivalDate = orderAdd.ArrivalDate,
            Street = orderAdd.Street!,
            City = orderAdd.City!,
            Country = orderAdd.Country,
            CustomerId = customerId.ToString(),
            TotalPrice = orderAdd.TotalPrice,
        };

        order.OrderProducts = orderAdd.OrderProducts!.Select(op => new OrderProduct()
        {
            OrderId = order.Id,
            ProductId = op.ProductId,
            ProductCount = op.ProductCount,
            Size = (Size)Enum.Parse<Size>(op.Size),
            Color = (Color)Enum.Parse<Color>(op.Color),
            Price = op.Price,
        }).ToList();

        _unitOfWork.OrderRepo.Add(order);

        /// Decrease Quantity
        /// 

        var cartProduct = _unitOfWork.CartRepo.GetCartProductByCustomerId(customerId).Products;



        var productsDetails = _unitOfWork.ProductsRepo.GetProductsInfo()
                .Where(p => cartProduct.Select(ps => ps.ProductId).Contains(p.ProductID)).ToList();

        for (int i = 0; i < productsDetails.Count; i++)
        {
            if (cartProduct.FirstOrDefault(p => p.ProductId == productsDetails[i].ProductID) != null
                && cartProduct.FirstOrDefault(p => p.Color == productsDetails[i].Color) != null
                && cartProduct.FirstOrDefault(p => p.Size == productsDetails[i].Size) != null)
            {
                productsDetails[i].Quantity -= cartProduct.FirstOrDefault(p => p.ProductId == productsDetails[i].ProductID).ProductCount;
            }
        }

        _unitOfWork.CartRepo.Delete(_unitOfWork.CartRepo.GetCartProductByCustomerId(customerId));
        return _unitOfWork.SaveChange() > 0;
    }

    public bool UpdateOrder(OrderUpdateDto orderUpdate)
    {
        Order? order = _unitOfWork.OrderRepo.GetById(orderUpdate.Id);
        if (order is null) return false;

        order.PaymentStatus = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), orderUpdate.PaymentStatus);
        order.PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), orderUpdate.PaymentMethod);
        order.OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderUpdate.OrderStatus);
        order.Discount = orderUpdate.Discount;
        order.ArrivalDate = orderUpdate.ArrivalDate;
        order.Street = orderUpdate.Street!;
        order.City = orderUpdate.City!;
        order.Country = (Countries)Enum.Parse(typeof(Countries), orderUpdate.Country);


        _unitOfWork.OrderRepo.Update(order); //add
        return _unitOfWork.SaveChange() > 0;
    }

    public OrderWithProductsAndCustomerReadDto? OrderWithProductsAndCustomerRead(Guid id)
    {
        Order? order = _unitOfWork.OrderRepo.GetOrderProductsAndCustomer(id);
        if (order is null) { return null; }

        return new OrderWithProductsAndCustomerReadDto
        {

            OrderData = order.OrderData,
            PaymentStatus = order.PaymentStatus,
            PaymentMethod = order.PaymentMethod,
            OrderStatus = order.OrderStatus,


            Street = order.Street,
            City = order.City,
            Country = order.Country,


            CustomerFname = order.Customer.FirstName,
            CustomerMname = order.Customer.MidName,
            CustomerLname = order.Customer.LastName,
            PhoneNumber = order.Customer.PhoneNumber,
            OrderProducts = order.OrderProducts.Select(op => new ProductChildReadDto
            {
                Id = op.ProductId,
                Name = op.Product!.Name,
                Description = op.Product!.Description,
                Price = op.Price,
                Discount = (double)op.Product.Discount,
                Color = op.Color.ToString(),
                Size = op.Size.ToString(),
                ProductCount = op.ProductCount
            }).ToList()
        };
    }

    public OrderWithProductsReadDto? OrderWithProductsRead(Guid id)
    {
        Order? order = _unitOfWork.OrderRepo.GetOrderProducts(id);
        if (order is null) { return null; }
        return new OrderWithProductsReadDto
        {
            Id = order.Id,
            OrderData = order.OrderData,
            PaymentStatus = order.PaymentStatus,
            PaymentMethod = order.PaymentMethod,
            OrderStatus = order.OrderStatus,
            Discount = order.Discount,
            ArrivalDate = order.ArrivalDate,
            Street = order.Street,
            City = order.City,
            Country = order.Country,

            OrderProducts = order.OrderProducts.Select(op => new ProductChildReadDto
            {
                Id = op.OrderId,
                Name = op.Product!.Name,
                Description = op.Product!.Description,
                Price = op.Product.Price,
                Discount = (double)op.Product.Discount,
            }).ToList()

        };
    }

    public bool DeleteOrder(Guid id)
    {
        Order? order = _unitOfWork.OrderRepo.GetById(id);
        if (order != null)
        {
            _unitOfWork.OrderRepo.Delete(order);
            return _unitOfWork.SaveChange() > 0;
        }
        return false;
    }

    public bool DeleteProductFromOrder(Guid orderId, Guid productId)
    {
        Order? order = _unitOfWork.OrderRepo.GetById(orderId);
        if (order == null)
        {
            return false;
        }
        OrderProduct? orderProduct = order.OrderProducts.FirstOrDefault(op => op.ProductId == productId);

        if (orderProduct == null)
        {
            return false;
        }
        _unitOfWork.OrderRepo.DeleteFromOrderProductsByProductId(orderProduct.OrderId);
        //order.OrderProducts.Remove(orderProduct);

        return _unitOfWork.SaveChange() > 0;
    }

    public bool CheckQuantityOfProductsBeforeOrder(Guid customerId)
    {
        var checkQuantity = _unitOfWork.CartRepo.GetCartProductByCustomerId(customerId).Products
            .FirstOrDefault(ps => ps.ProductCount <= _unitOfWork.ProductsRepo.GetProductDetails(ps.ProductId).Product_Color_Size_Quantity
            .FirstOrDefault(p => p.Color == ps.Color && p.Size == ps.Size).Quantity);
        if (checkQuantity == null)
        {
            return false;
        }

        return true;
    }

    public List<OrderWithProductsReadDto> GetOrdersByCustomerId(string customerId)
    {
        List<Order> orders = _unitOfWork.OrderRepo.GetOrdersByCustomerId(customerId);

        if (orders is null)
        {
            return null!;
        }

        return orders.Select(o => new OrderWithProductsReadDto
        {
            Id = o.Id,
            OrderData = o.OrderData,
            PaymentStatus = o.PaymentStatus,
            PaymentMethod = o.PaymentMethod,
            OrderStatus = o.OrderStatus,
            Discount = o.Discount,
            ArrivalDate = o.ArrivalDate,
            Street = o.Street,
            City = o.City,
            Country = o.Country,
            TotalPrice = o.TotalPrice,

            OrderProducts = o.OrderProducts.Select(op => new ProductChildReadDto
            {
                Id = op.OrderId,
                Name = op.Product!.Name,
                Description = op.Product!.Description,
                Price = op.Product.Price,
                Discount = (double)op.Product.Discount,
            }).ToList()
        }).ToList();
    }

    public bool UpdateOrderStatus(Guid orderId, OrderStatus orderStatus)
    {
        Order? order = _unitOfWork.OrderRepo.GetById(orderId);
        if (order is null) return false;

        order.OrderStatus = orderStatus;

        _unitOfWork.OrderRepo.Update(order);
        return _unitOfWork.SaveChange() > 0;
    }
}