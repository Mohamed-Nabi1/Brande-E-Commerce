
namespace E_Commerce.DAL
{
    public interface ICartRepo : IGenericRepo<Cart>

    {
       Cart? GetCartProductByCustomerId(Guid CustomerId);
       
    }
}
