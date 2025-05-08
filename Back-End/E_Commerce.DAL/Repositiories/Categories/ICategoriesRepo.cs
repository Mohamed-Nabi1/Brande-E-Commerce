namespace E_Commerce.DAL;

public interface ICategoriesRepo : IGenericRepo<Category>
{

    List<Category>? GetParentCategory();
    List<Category>? GetSubCategories(Guid parentCategoryId);
    Category? GetProductsForCategory(Guid categorytId);
    List<Category>? GetAllCategoriesWithAllPrdoucts();
    Category? GetCategoryById(string categorytId);
    Category? GetProductsByCategoryId(Guid categoryId);
    List<Product>? GetProductsByName(string productName);

     List<Product> GetProductsByParentCategory(Guid parentCategoryId);
     List<Category> GetCategoriesUnique();


}
