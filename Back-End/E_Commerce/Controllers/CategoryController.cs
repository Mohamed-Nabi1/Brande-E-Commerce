using E_Commerce.BL;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{

    #region CTOR

    private readonly ICategoriesManager _categoryManager;

    public CategoryController(ICategoriesManager categoryManager)
    {
        _categoryManager = categoryManager;
    }
    #endregion

    #region All Categories

    [HttpGet]
    public ActionResult<List<CategoryReadDto>> AllCategories()
    {
        List<CategoryReadDto> categories = _categoryManager.GetAllCategories();
        return categories;
    }
    #endregion

    #region Add Category


    [HttpPost("Add")]
    public ActionResult AddCategory(CategoryAddDto category)
    {

        bool request = _categoryManager.AddCategory(category);

        if (request == false)
        {
            return BadRequest();
        }
        return Ok();

    }
    #endregion

    #region Delete Category

    [HttpDelete("Delete/{id}")]
    public ActionResult DeleteCategory(Guid id)
    {

        bool request = _categoryManager.DeleteCategory(id);
        if (request == false)
        {
            return BadRequest();
        }
        return Ok("Category Deleted Successfully");

    }


    #endregion

    #region Update Category

    [HttpGet("Update/{id}")]
    public ActionResult<CategoryUpdateDto> CategoryToUpdate(Guid id)
    {

        CategoryUpdateDto? category = _categoryManager.CategoryToUpdate(id);
        if (category == null)
        {

            return BadRequest();
        }
        return category;

    }

    [HttpPut("Update")]
    public ActionResult UpdateCategory(CategoryUpdateDto categoryUpdateDto)
    {

        bool request = _categoryManager.UpdateCategory(categoryUpdateDto);
        if (request == false)
        {
            return BadRequest();
        }
        return Ok("Category Updated Successfully");

    }

    #endregion

    #region Get Category By Id

    [HttpGet]
    [Route("Details/{id}")]
    public ActionResult<CategoryReadDto> CategoryDetail(Guid id)
    {

        CategoryReadDto? categoryDetails = _categoryManager.GetCategoryById(id);
        if (categoryDetails == null)
        {
            return BadRequest();
        }

        return categoryDetails;
    }

    #endregion

    #region Get subcategories for Parent Category

    [HttpGet]
    [Route("subCategories/{id}")]
    public ActionResult<List<SubCategoryReadDto>> GetSubCategories(Guid id)
    {
        List<SubCategoryReadDto> subCategories = _categoryManager.GetSubCategories(id).ToList();

        if (subCategories == null)
        {
            return BadRequest();
        }


        return subCategories;
    }
    #endregion

    #region get Parent Categories

    [HttpGet("ParentCategories")]
    public ActionResult<List<CategoryReadDto>> AllParentCategories()
    {
        List<CategoryReadDto> categories = _categoryManager.GetParentCategory();
        return categories;
    }


    #endregion


    #region get products for category

    [HttpGet("PrdouctsForCategory/{id}")]
    public ActionResult<List<ProductDetailsReadDto>> ProductsForCategory(Guid id)
    {
        List<ProductDetailsReadDto> products = _categoryManager.GetProductsByCategoryIds(id);
        return products;
    }


    #endregion

    #region get products by parent category id


    [HttpGet("PrdouctByParentCategory/{id}")]
    public ActionResult<List<ProductDetailsReadDto>> ProductsByParentCategoryID(Guid id)
    {
        List<ProductDetailsReadDto> products = _categoryManager.GetProductsByParentCategoryIds(id);
        return products;
    }


    #endregion
    [HttpGet("CategoryUnique")]
    public ActionResult<List<CategoryReadDto>> GetCategoriesUnique() 
    {
        List<CategoryReadDto> products = _categoryManager.GetCategoriesUnique();
        return products;
    }

    

}
