using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.DAL;

[PrimaryKey(nameof(ProductId), nameof(CustomerId))]
public class CustomerReview
{
    public string CustomerId { get; set; } = null!;
    public Customer Customer { get; set; } = null!;

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public string Description { get; set; } = null!;
    [Range(0, 5)]
    public int Rate { get; set; }
    public DateTime CreatedTime { get; set; }
}
