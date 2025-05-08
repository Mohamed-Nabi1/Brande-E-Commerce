using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL;

public class ProductPaginationDto
{

    public List<ProductCategoryPagination> items { get; set; }= new List<ProductCategoryPagination>();
    public int totalCount { get; set; }
}
