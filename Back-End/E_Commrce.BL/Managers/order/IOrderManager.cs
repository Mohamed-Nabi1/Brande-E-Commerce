using E_Commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL;

public interface IOrderManager
{
    OrderWithProductsReadDto? OrderWithProductsRead(Guid id);
    
    OrderWithProductsAndCustomerReadDto? OrderWithProductsAndCustomerRead(Guid id);
    
    List<OrderReadDto> GetAllOrders();
    
    List<OrderReadDto> GetAllOrderswithCustName();
    
    OrderReadDto GetOrderById(Guid id);
    
    bool AddOrder(OrderAddDto orderAddDto ,Guid customerId);  //[1]
    
    bool UpdateOrder(OrderUpdateDto orderUpdateDto);  //[2]

    bool UpdateOrderStatus(Guid orderId, OrderStatus orderStatus);


    bool DeleteOrder(Guid id);
    //new
    bool DeleteProductFromOrder(Guid orderId, Guid productId);

    bool CheckQuantityOfProductsBeforeOrder(Guid customerId);
    
    List<OrderWithProductsReadDto> GetOrdersByCustomerId(string customerId);



    //[1] +add (products)
    //[2] +update count of prouducts in(orderProduct table)
    //remove some products in order;
}
