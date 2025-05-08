using E_Commerce.DAL;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.BL;

public class WishListManager : IWishListManager
{
    private readonly IUnitOfWork unitOfWork;

    public WishListManager(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public bool AddToWishList(WishListIDsDto IDsDto,Guid customerId)
    {
        var product = unitOfWork.ProductsRepo.GetById(IDsDto.ProductId);
        if (product is null) 
            return false;

        var wishlist = unitOfWork.WishListRepo.GetWishListProducts(customerId);
        if (wishlist!.Products.Contains(product))
            return false;

        wishlist.Products.Add(product);
        return unitOfWork.SaveChange() > 0;
    }

    public bool DeleteFromWishList(WishListIDsDto IDsDto, Guid customerId)
    {

        var product = unitOfWork.ProductsRepo.GetById(IDsDto.ProductId);
        if (product is null)
            return false;

        var wishlist = unitOfWork.WishListRepo.GetWishListProducts(customerId);
        if(! wishlist!.Products.Contains(product))
            return false;

        wishlist.Products.Remove(product);
        return unitOfWork.SaveChange() > 0;
    }

    public WishListDisplayDto GetWishList(Guid customerId)
    {
        var wishlist = unitOfWork.WishListRepo.GetWishListProducts(customerId);
        return new WishListDisplayDto
        {
            CustomerName = $"{wishlist.Customer.FirstName} {wishlist.Customer.LastName}",
            Products = wishlist.Products.Select(p => new WishListProductsDto
            {
                ProductId=p.Id,
                ProductName = p.Name,
                Price = p.Price,
                Discount = p.Discount,
                ImageUrl = p.ProductImages.FirstOrDefault(img => img.ProductID == p.Id).ImageURL
             //   --> null refrence exception in ProductImg Due to No inserted images
            }).ToList()
        };
    }
}
