using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL;



[PrimaryKey(nameof(ProductID), nameof(ImageURL))]
public class Product_IMG
{
    public Guid ProductID { get; set; }
    public Product Product { get; set; } = null!;

    public string ImageURL { get; set; } = string.Empty;

}
