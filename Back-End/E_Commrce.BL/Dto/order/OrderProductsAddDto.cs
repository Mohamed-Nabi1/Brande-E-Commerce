using E_Commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL;

//NotComplete
public class OrderProducts
{
    public int ProductCount { get; set; }
    public Guid ProductId { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public decimal Price { get; set; }
}
