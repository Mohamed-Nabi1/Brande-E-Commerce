using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL;

public class CustomerReviewRepo : GenericRepo<CustomerReview>, ICustomerReviewRepo
{
    private readonly E_CommerceContext context;

    public CustomerReviewRepo(E_CommerceContext context) : base(context)
    {
        this.context = context;
    }

    public List<CustomerReview> GetAllReviewsCustomersProducts()
    {
        return context.Set<CustomerReview>()
                        .Include(cr => cr.Product)
                        .Include(cr => cr.Customer)
                        .ToList();
    }

    public CustomerReview? GetByIds(Guid customerId, Guid productId)
    {
        return context.Set<CustomerReview>()
                        .FirstOrDefault(cr => cr.CustomerId == customerId.ToString() && cr.ProductId == productId);
    }

    public CustomerReview? GetByIdsWithCustomerProduct(Guid customerId, Guid productId)
    {
        return context.Set<CustomerReview>()
                        .Include(cr=>cr.Customer)
                        .Include(cr=>cr.Product)
                        .FirstOrDefault(cr => cr.CustomerId == customerId.ToString() && cr.ProductId == productId);
    }

    public List<CustomerReview> GetCustomerProductsReviews(Guid customerId)
    {
        return context.Set<CustomerReview>()
                        .Include(cr => cr.Product)
                        .Where(cr => cr.CustomerId == customerId.ToString())
                        .ToList();
    }

    public List<CustomerReview> GetProductCustomersReviews(Guid productId)
    {
        return context.Set<CustomerReview>()
                        .Include(cr => cr.Customer)
                        .Where(cr => cr.ProductId == productId)
                        .ToList();
    }
}