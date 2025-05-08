namespace E_Commerce.DAL;

public class WishList
{
    public Guid Id { get; set; }

    public Customer Customer { get; set; } = null!;
    public List<Product> Products { get; set; } = new List<Product>();
}
