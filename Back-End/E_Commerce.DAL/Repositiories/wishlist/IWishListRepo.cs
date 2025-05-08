namespace E_Commerce.DAL;

public interface IWishListRepo:IGenericRepo<WishList>
{
    WishList GetWishListProducts(Guid customerId);
}
