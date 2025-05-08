using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL;

public class ProductReviewsDto
{
    public string CustomerName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Rate { get; set; }
    public DateTime CreatedTime { get; set; }
}
