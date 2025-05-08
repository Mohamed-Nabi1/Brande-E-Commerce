
namespace E_Commerce.DAL
{
     public interface ICartProductRepo : IGenericRepo<CartProduct> 
    {
        CartProduct? GetById(Guid productId,Guid cartId);
        CartProduct? GetCartProductByCustomerId(Guid CustomerId);

    }
}
