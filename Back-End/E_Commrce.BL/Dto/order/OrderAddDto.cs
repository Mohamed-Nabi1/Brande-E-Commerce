using E_Commerce.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL;

public class OrderAddDto
{
    public DateTime OrderData { get; set; } = DateTime.Now;
    public string PaymentStatus { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    [Range(0, 1)]
    public double? Discount { get; set; } = 0;
    public DateTime ArrivalDate { get; set; } = DateTime.Now.AddDays(5);
    public string? Street { get; set; }
    public string? City { get; set; }
    public Countries Country { get; set; }

    public List<OrderProducts>? OrderProducts { get; set; }
    public decimal TotalPrice { get; set; }
}
