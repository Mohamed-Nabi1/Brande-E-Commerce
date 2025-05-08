 
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL;


[PrimaryKey(nameof(CartId))]
public class Cart
{
    public Guid CartId { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public List<CartProduct> Products { get; set; } = null!;
}
