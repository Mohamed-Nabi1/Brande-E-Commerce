

namespace E_Commerce.DAL
{
    public interface IOrderRepo :IGenericRepo<Order>
    {
        List<Order> GetOrdersWithCustomer();
        Customer? GetCustomerByOrderId(Guid id);
        Order? GetOrderProducts(Guid id);
        Order? GetOrderProductsAndCustomer(Guid id);
        Order? GetOrderDetails(Guid id);
        void AddOrderProductsRange(List<OrderProduct> orderProductsToAdd);
        void DeleteRangeOfOrderProduct(List<OrderProduct> orderProductsToDelete);
        void DeleteFromOrderProductsByProductId(Guid id);

        //
        List<Order> GetOrdersByCustomerId(string customerId);

    }
}
