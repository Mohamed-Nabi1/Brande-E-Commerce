namespace E_Commerce.BL;

public class ProductDetailsReadDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Rate { get; set; }
    public List<ProductImageDto> ProductImages { get; set; } = new List<ProductImageDto>();
    public List<ProductInfoDto> ProductInfo { get; set; } = new List<ProductInfoDto>();
}
