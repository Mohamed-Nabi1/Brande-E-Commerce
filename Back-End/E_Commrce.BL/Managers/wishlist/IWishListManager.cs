namespace E_Commerce.BL;

public interface IWishListManager
{
    bool AddToWishList(WishListIDsDto IDsDto, Guid customerId);
    bool DeleteFromWishList(WishListIDsDto IDsDto, Guid customerId);
    WishListDisplayDto GetWishList(Guid customerId);
}
