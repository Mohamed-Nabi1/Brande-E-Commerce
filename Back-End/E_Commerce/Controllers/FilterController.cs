using E_Commerce.BL;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : Controller
    {
        private readonly IFilterManger _filterManger;

        public FilterController(IFilterManger filterManger)
        {
            _filterManger = filterManger;
        }
        [HttpGet]

        public ActionResult<List<ProductDetailsReadDto>> FilterProducts([FromQuery] FilterDto filter)
        {
            var products = _filterManger.GetFilteredProducts(filter);
            
            return products;
        }
    }
}
