using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL;

public class ProductCategoryPagination
{


    public string? CategoryName { get; set; }
    public int Count { get; set; }

    public List<ProductDetailsReadDto>? Products { get; set; }




}
