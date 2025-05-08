using E_Commerce.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL;

public class OrderReadDto
{
    public Guid Id { get; set; }
    public DateTime OrderData { get; set; }
    public string? PaymentStatus { get; set; }
    public string? PaymentMethod { get; set; }
    public string? OrderStatus { get; set; }
    [Range(0, 1)]
    public double Discount { get; set; }
    public DateTime ArrivalDate { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public String? CustomerName { get; set; }
    public decimal TotalPrice { get; set; }
}
