using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL;

public class CustomerReviewAddDto
{
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    [Required]
    [Range(0, 5)]
    public int Rate { get; set; }
}
