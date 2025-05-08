namespace E_Commerce.BL;

public class WishListDisplayDto
{
    public string CustomerName { get; set; } = string.Empty;
    public List<WishListProductsDto> Products { get; set; } = null!;
}
