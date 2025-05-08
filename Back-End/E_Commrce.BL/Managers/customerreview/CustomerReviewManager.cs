using E_Commerce.DAL;
using System.Reflection.Metadata.Ecma335;

namespace E_Commerce.BL;

public class CustomerReviewManager : ICustomerReviewManager
{
    private readonly IUnitOfWork unitOfWork;

    public CustomerReviewManager(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public bool AddCustomerReview(CustomerReviewAddDto addDto)
    {
        unitOfWork.CustomerReviewRepo.Add(new CustomerReview
        {
            CustomerId = addDto.CustomerId.ToString(),
            ProductId = addDto.ProductId,
            CreatedTime = DateTime.Now,
            Description = addDto.Description,
            Rate = addDto.Rate,
        });
        return unitOfWork.SaveChange() > 0;
    }

    public bool DeleteCustomerReview(CustomerReviewIDsDto customerReviewIDs)
    {
        var customerReview = unitOfWork.CustomerReviewRepo.GetByIds(customerReviewIDs.CustomerId, customerReviewIDs.ProductId);
        if(customerReview is null) return false;
        
        unitOfWork.CustomerReviewRepo.Delete(customerReview);
        return unitOfWork.SaveChange() > 0;
    }

    public List<CustomerReviewDetailsDto> GetAll()
    {
        return unitOfWork.CustomerReviewRepo.GetAllReviewsCustomersProducts().Select(cr => new CustomerReviewDetailsDto
        {
            CreatedTime = cr.CreatedTime,
            Description = cr.Description,
            Rate = cr.Rate,
            CustomerName = $"{cr.Customer.FirstName} {cr.Customer.LastName}",
            ProductName = cr.Product.Name
        }).ToList();
    }

    public List<CustomerReviewDetailsDto> GetCustomerProductsReviews(Guid customerId)
    {
        return unitOfWork.CustomerReviewRepo.GetCustomerProductsReviews(customerId).Select(cr => new CustomerReviewDetailsDto
        {
            CreatedTime = cr.CreatedTime,
            Description = cr.Description,
            Rate = cr.Rate,
            ProductName = cr.Product.Name
        }).ToList();
    }

    public CustomerReviewDetailsDto GetCustomerReviewDetails(CustomerReviewIDsDto customerReviewIDs)
    {
        var customerReview =  unitOfWork.CustomerReviewRepo.GetByIdsWithCustomerProduct(customerReviewIDs.CustomerId, customerReviewIDs.ProductId);

        return customerReview is not null ? new CustomerReviewDetailsDto
        {
            CreatedTime = customerReview.CreatedTime,
            Description = customerReview.Description,
            CustomerName = $"{customerReview.Customer.FirstName} {customerReview.Customer.LastName}",
            ProductName = customerReview.Product.Name,
            Rate = customerReview.Rate
        }: new () ;
    }

    public List<CustomerReviewDetailsDto> GetProductCustomersReviews(Guid productId)
    {
        return unitOfWork.CustomerReviewRepo.GetProductCustomersReviews(productId).Select(cr => new CustomerReviewDetailsDto
        {
            CreatedTime = cr.CreatedTime,
            Description = cr.Description,
            Rate = cr.Rate,
            CustomerName = $"{cr.Customer.FirstName} {cr.Customer.MidName} {cr.Customer.LastName}"
        }).ToList();
    }

    public bool UpdateCustomerReview(CustomerReviewUpdateDto updateDto)
    {
        var customerReview = unitOfWork.CustomerReviewRepo.GetByIds(updateDto.CustomerId, updateDto.ProductId);
        if(customerReview is null) return false;

        customerReview.Description = updateDto.Description;
        customerReview.Rate = updateDto.Rate;

        unitOfWork.CustomerReviewRepo.Update(customerReview);
        return unitOfWork.SaveChange()>0;
    }
}
