namespace E_Commerce.BL;

public interface IProductManager
{
    List<ProductReadDto> AllProducts();

    List<ProductWithImagesDto> ProductsStartsWith(string query);
    
    bool Add(ProductAddDto productAdd);
    
    bool Delete(Guid productId);
    
    ProductDetailsReadDto? ProductDetails(Guid productId);
    
    List<ProductReviewsDto>? ProductReviews(Guid productId);
    
    List<ProductCategories>? ProductCategories(Guid productId);
    
    ProductUpdateDto? ProductToUpdate(Guid productId);
    
    bool Update(ProductUpdateDto productUpdate);

    ProductAfterFillterByColor ProductFillterByColor(ProductFillterByColor productDto);

    ProductPaginationDto AllProductsPagination(int page, int countPerPage);

    List<ProductWithImagesDto> ProductsWithImages();

    ProductDetailsDistinctDto? ProductDetailsDistinct(Guid productId);

    List<ProductWithImagesDto> GetProductsUnique();
}
