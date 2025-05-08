using E_Commerce.BL;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly ICustomerReviewManager customerReviewManager;

    public ReviewsController(ICustomerReviewManager customerReviewManager)
    {
        this.customerReviewManager = customerReviewManager;
    }

    [HttpGet]
    public ActionResult<List<CustomerReviewDetailsDto>> GetAll()
    {
        return customerReviewManager.GetAll();
    }

    [HttpGet]
    [Route("Details")]
    public ActionResult<CustomerReviewDetailsDto> Get([FromQuery] CustomerReviewIDsDto id)
    {
        return (id.CustomerId == Guid.Empty && id.ProductId == Guid.Empty) ? BadRequest() : customerReviewManager.GetCustomerReviewDetails(id);
    }

    [HttpGet]
    [Route("Customer/{customerId}")]
    public ActionResult<List<CustomerReviewDetailsDto>> GetProductsReviews(Guid customerId)
    {
        if (customerId == Guid.Empty)
            return BadRequest("Not Valid ID");
        return customerReviewManager.GetCustomerProductsReviews(customerId);
    }

    [HttpGet]
    [Route("Product/{productId}")]
    public ActionResult<List<CustomerReviewDetailsDto>> GetCustomersReviews(Guid productId)
    {
        if(productId == Guid.Empty)
            return BadRequest("Not Valid ID");
        return customerReviewManager.GetProductCustomersReviews(productId);
    }

    [HttpPost]
    public ActionResult Add(CustomerReviewAddDto addDto)
    {
        return customerReviewManager.AddCustomerReview(addDto) ? Ok("Added Succcessfully") : BadRequest();
    }

    [HttpPut]
    public ActionResult Update([FromQuery] CustomerReviewIDsDto iDsDto, CustomerReviewUpdateDto updateDto)
    {
        if (!(iDsDto.CustomerId == updateDto.CustomerId && iDsDto.ProductId == updateDto.ProductId))
            return BadRequest();

        return (customerReviewManager.UpdateCustomerReview(updateDto)) ? Ok("Updated Successfully") : NoContent();
    }

    [HttpDelete]
    public ActionResult Delete([FromQuery] CustomerReviewIDsDto iDsDto)
    {
        return customerReviewManager.DeleteCustomerReview(iDsDto) ? Ok("Deleted Successfully") : NotFound();
    }
}
