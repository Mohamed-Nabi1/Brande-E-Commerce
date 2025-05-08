using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.DAL;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderData { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatus OrderStatus { get; set; }
    [Range(0, 1)]
    public double Discount { get; set; }
    public DateTime ArrivalDate { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public Countries Country { get; set; }

    [ForeignKey(nameof(Customer))]
    public string CustomerId { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public decimal TotalPrice { get; set; }
}
