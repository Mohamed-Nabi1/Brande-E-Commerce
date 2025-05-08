using E_Commerce.DAL;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.BL
{
    public class FilterManger : IFilterManger
    {
        private readonly IUnitOfWork _unitOfWork;

        public FilterManger(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<ProductDetailsReadDto> GetFilteredProducts(FilterDto filter)
        {
            List<Product> filterdproducts = _unitOfWork.ProductsRepo.GetAllProductsWithDetails()
                        .Where(p => filter.ChildId.IsNullOrEmpty() ? true : !filter.ChildId.Intersect(p.Categories.Select(c => c.Id)).IsNullOrEmpty())
                        .Where(p => filter.ParentId.IsNullOrEmpty() ? true : !filter.ParentId.Intersect(p.Categories.Select(c => (Guid)c.ParentCategoryId)).IsNullOrEmpty())
                        .Where(p => filter.Size.IsNullOrEmpty() ? true : !filter.Size.Intersect(p.Product_Color_Size_Quantity.Select(i => i.Size.ToString())).IsNullOrEmpty())
                        .Where(p => filter.Color.IsNullOrEmpty() ? true : !filter.Color.Intersect(p.Product_Color_Size_Quantity.Select(i => i.Color.ToString())).IsNullOrEmpty())
                        .Where(p => filter.MinPrice==null ? true : filter.MinPrice<=p.Price)
                        .Where(p => filter.MaxPrice==null ? true : filter.MaxPrice>=p.Price)
                        .Where(p => filter.Rate.IsNullOrEmpty() ? true : !filter.Rate.Contains(p.Rate))
                        .ToList();
            var filterdproducts2 = _unitOfWork.ProductsRepo.GetAllProductsWithDetails()
                 .Where(p => filter.Color.IsNullOrEmpty() ? true : !filter.Color.Intersect(p.Product_Color_Size_Quantity.Select(i => i.Color.ToString())).IsNullOrEmpty());
            var FilterdproductsDto = filterdproducts.Select(p=> new ProductDetailsReadDto
            {
                Id = p.Id,
                Description = p.Description,
                Name = p.Name,
                Rate = p.Rate,
                Discount = p.Discount,
                Price = p.Price,
                ProductImages = p.ProductImages.Select(i => new ProductImageDto {
                    ImageURL = i.ImageURL,
                }).ToList(),
                ProductInfo = p.Product_Color_Size_Quantity.Select(i=>new ProductInfoDto
                {
                    Color= i.Color.ToString(),
                    Size= i.Size.ToString(),
                    Quantity= i.Quantity
                }).ToList(),
            }).ToList();

            return FilterdproductsDto;
        }
    }
}
