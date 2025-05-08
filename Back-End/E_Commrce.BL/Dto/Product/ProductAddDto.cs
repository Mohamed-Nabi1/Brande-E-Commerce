using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.BL;

public class ProductAddDto
{
    [MinLength(3)]
    [MaxLength(30)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    [Range(0, 1)]
    public decimal Discount { get; set; } = 0;

    public List<string> Images { get; set; } = null!;
    public List<ProductInfoDto> ProductInfo { get; set; } = null!;
    public List<Guid> ProductCategories { get; set; } = null!;
}
