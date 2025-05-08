using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL;

public class WishListRepo : GenericRepo<WishList>, IWishListRepo
{
    private readonly E_CommerceContext context;

    public WishListRepo(E_CommerceContext context) : base(context)
    {
        this.context = context;
    }


    public WishList GetWishListProducts(Guid customerId)
    {
        var wishlist = context.Set<WishList>()
                        .Include(wl => wl.Products)
                            .ThenInclude(p => p.ProductImages)
                        .Include(wl=>wl.Customer)    
                        .FirstOrDefault(wl => wl.Customer.Id == customerId.ToString());

        if(wishlist is null)
        {
            wishlist = new WishList
            {
                Id = Guid.NewGuid(),
                Customer = context.Set<Customer>().FirstOrDefault(c => c.Id == customerId.ToString())!,
                Products = new List<Product>()
            };
            context.Set<WishList>().Add(wishlist);
        }
        return wishlist;
    }

    
}
