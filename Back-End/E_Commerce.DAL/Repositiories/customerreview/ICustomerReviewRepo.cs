namespace E_Commerce.DAL;

public interface ICustomerReviewRepo:IGenericRepo<CustomerReview>
{
    CustomerReview? GetByIdsWithCustomerProduct(Guid customerId,Guid productId);
    CustomerReview? GetByIds(Guid customerId,Guid productId);
    List<CustomerReview> GetCustomerProductsReviews(Guid customerId);
    List<CustomerReview> GetProductCustomersReviews(Guid productId);
    List<CustomerReview> GetAllReviewsCustomersProducts();
}
