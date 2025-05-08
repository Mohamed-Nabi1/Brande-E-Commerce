using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL
{
    public interface IFilterManger
    {
        List<ProductDetailsReadDto> GetFilteredProducts(FilterDto filter);
    }
}
