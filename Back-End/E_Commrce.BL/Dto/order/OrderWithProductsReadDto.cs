using E_Commerce.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL
{
    public class OrderWithProductsReadDto
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
        public List<ProductChildReadDto>? OrderProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}