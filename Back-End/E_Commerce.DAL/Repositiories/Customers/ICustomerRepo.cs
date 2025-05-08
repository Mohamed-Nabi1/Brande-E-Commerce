namespace E_Commerce.DAL
{
    public interface ICustomerRepo : IGenericRepo<Customer>
    {
        Customer? GetOrdersByCustomerId(Guid Id);
        Customer? GetCustomerCartByCustomerId(Guid Id);
        Customer? GetWishListByCustomerId(Guid Id);
        Customer? GetById(string id);


    }
}
