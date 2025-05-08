using E_Commerce.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager productManager;

        public ProductsController(IProductManager _productManager)
        {
            productManager = _productManager;
        }

        [HttpGet("Pagination/{page}/{countPerPage}")]
        public ActionResult<ProductPaginationDto> AllProductsPagination(int page, int countPerPage)
        {

            return productManager.AllProductsPagination(page, countPerPage);
        }
        [HttpGet]
        [Route("ProductWithImages")]
        public ActionResult<List<ProductWithImagesDto>> AllProductsWithImages()
        {
            List<ProductWithImagesDto> products = productManager.ProductsWithImages();
            return products;
        }

        [HttpGet]
        public ActionResult<List<ProductReadDto>> AllProducts()
        {
            List<ProductReadDto> products = productManager.AllProducts();
            return products;
        }

        [HttpGet]
        [Route("SearchForProducts")]
        public ActionResult<List<ProductWithImagesDto>> SearchForProducts(string query)
        {
            var products = productManager.ProductsStartsWith(query);
            return products;
        }

        [HttpPost]
        [Route("Images")]
        public async Task<IActionResult> UploadImage(List<IFormFile> Images)
        {
            List<string> urls = new List<string>();
            foreach (var image in Images)
            {
                #region Extention Checking
                var extension = image.ContentType.Split('/')[1];
                //var extension = Path.GetExtension(image.ContentType);
                var extensionList = new string[]
                {
                "png",
                "jpg",
                "jpeg",
                "svg"
                };

                bool isExtensionAllowed = extensionList.Contains(extension,
                    StringComparer.InvariantCultureIgnoreCase);
                if (!isExtensionAllowed)
                {
                    return BadRequest("Format not allowed");
                }
                #endregion

                #region Length Checking

                bool isSizeAllowed = image.Length is > 0 and < 5_000_000; //Picture Size (Minimum: >0 and Max: 5MB)

                if (!isSizeAllowed)
                {
                    return BadRequest("Size is Larger than allowed size");
                }
                #endregion

                #region Storing Image

                var generatedPictureName = $"{Guid.NewGuid()}.{extension}";
                var savedPicturesPath = Environment.CurrentDirectory + "\\Images\\" + generatedPictureName;
                using (var stream = new FileStream(savedPicturesPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                #endregion

                #region URL Generating
                var url = $"{Request.Scheme}://{Request.Host}/Images/{generatedPictureName}";
                #endregion

                urls.Add(url);
            }


            return Ok(urls);
        }


        [HttpPost]
        [Route("Add")]
        public ActionResult AddProduct(ProductAddDto product)
        {
            return productManager.Add(product)? Ok():BadRequest();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult Delete(Guid id)
        {
            bool request = productManager.Delete(id);
            if (!request)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet]
        [Route("Details/{id}")]
        public ActionResult<ProductDetailsReadDto> Details(Guid id)
        {
            ProductDetailsReadDto? productDetails = productManager.ProductDetails(id);
            if (productDetails is null)
            {
                return BadRequest();
            }

            return productDetails;
        }

        [HttpGet]
        [Route("Reviews/{id}")]
        public ActionResult<List<ProductReviewsDto>> Reviews(Guid id)
        {
            List<ProductReviewsDto>? reviews = productManager.ProductReviews(id);

            if (reviews is null)
            {
                return BadRequest();
            }
            else if (reviews.IsNullOrEmpty())
            {
                return NoContent();
            }
            return reviews!;
        }

        [HttpGet]
        [Route("Categories/{id}")]
        public ActionResult<List<ProductCategories>> Categories(Guid id)
        {
            List<ProductCategories>? categories = productManager.ProductCategories(id);
            if (categories is null)
            {
                return BadRequest();
            }
            else if (categories.IsNullOrEmpty())
            {
                return NoContent();
            }

            return categories!;
        }

        [HttpGet]
        [Route("Update/{id}")]
        public ActionResult<ProductUpdateDto> ProductToUpdate(Guid id)
        {
            ProductUpdateDto? product = productManager.ProductToUpdate(id);
            if (product is null)
            {
                return BadRequest();
            }
            return product;
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(ProductUpdateDto productUpdate)
        {
            bool request = productManager.Update(productUpdate);
            if (!request)
            {
                return BadRequest();
            }
            return Ok();
        }
        /*/////*/



        [HttpGet]
        [Route("FillterByColor")]
        public ActionResult<ProductAfterFillterByColor> FillterByColor([FromQuery] ProductFillterByColor productDto)
        {
            ProductAfterFillterByColor? request = productManager.ProductFillterByColor(productDto);
            if (request==null)
            {
                return BadRequest();
            }
            return request;
        }

        [HttpGet]
        [Route("ColorDistinct/{id}")]
        public ActionResult<ProductDetailsDistinctDto> DetailsDistinct(Guid id)
        {
            ProductDetailsDistinctDto? productDetails = productManager.ProductDetailsDistinct(id);
            if (productDetails is null)
            {
                return BadRequest();
            }

            return productDetails;
        }

        [HttpGet]
        [Route("UniqueProducts")]
        public ActionResult<List<ProductWithImagesDto>> GetProductsUnique()
        {
            return productManager.GetProductsUnique();
        }

    }
}
