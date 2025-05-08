using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL;

public class ProductReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; } = 0;
    public decimal Rate { get; set; } = 0;

}
