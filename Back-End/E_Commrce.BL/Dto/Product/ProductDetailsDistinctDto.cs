namespace E_Commerce.BL;

public class ProductDetailsDistinctDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Rate { get; set; }
    public List<ProductImageDto> ProductImages { get; set; } = new List<ProductImageDto>();
    public List<ProductInfoColorDistinctDto> ProductInfo { get; set; } = new List<ProductInfoColorDistinctDto>();
}
