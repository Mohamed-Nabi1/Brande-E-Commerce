namespace E_Commerce.BL;

public interface ICategoriesManager
{


    bool AddCategory(CategoryAddDto addCategoryDto);
    bool UpdateCategory(CategoryUpdateDto updateCategoryDto);
    CategoryUpdateDto? CategoryToUpdate(Guid CategoryId);
    bool DeleteCategory(Guid CategoryId);
    List<CategoryReadDto> GetAllCategories();
    CategoryReadDto? GetCategoryById(Guid CategoryId);

    List<SubCategoryReadDto>? GetSubCategories(Guid parentCategoryId);
    List<CategoryReadDto> GetParentCategory ();

    List<ProductReadDto>? GetProductsByCategoryId(Guid categoryId);
    List<ProductDetailsReadDto>? GetProductsByCategoryIds(Guid categoryId);
    List<ProductReadDto>? GetProductsByName(string ProductName);

    public List<ProductDetailsReadDto>? GetProductsByParentCategoryIds(Guid categoryId);

    List<CategoryReadDto> GetCategoriesUnique();








}
