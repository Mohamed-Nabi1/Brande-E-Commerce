using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DAL;

[PrimaryKey(nameof(ProductId), nameof(OrderId), nameof(Color), nameof(Size))]
public class OrderProduct
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }
    public int ProductCount { get; set; }
    public Color Color { get; set; }
    public Size Size { get; set; }
    public decimal Price { get; set; }
}
