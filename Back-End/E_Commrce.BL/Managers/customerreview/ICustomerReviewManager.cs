namespace E_Commerce.BL;

public interface ICustomerReviewManager
{
    List<CustomerReviewDetailsDto> GetAll();
    CustomerReviewDetailsDto GetCustomerReviewDetails(CustomerReviewIDsDto customerReviewIDs);
    List<CustomerReviewDetailsDto> GetCustomerProductsReviews(Guid customerId);
    List<CustomerReviewDetailsDto> GetProductCustomersReviews(Guid productId);
    bool AddCustomerReview(CustomerReviewAddDto addDto);
    bool DeleteCustomerReview(CustomerReviewIDsDto customerReviewIDs);
    bool UpdateCustomerReview(CustomerReviewUpdateDto updateDto);
}
