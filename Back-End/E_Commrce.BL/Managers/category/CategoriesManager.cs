using E_Commerce.DAL;

namespace E_Commerce.BL;

public class CategoriesManager : ICategoriesManager
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    #region Get All Categories
    public List<CategoryReadDto> GetAllCategories()
    {
        List<Category>? categoriesFromDb = _unitOfWork.CategoriesRepo.GetAllCategoriesWithAllPrdoucts();
        if (categoriesFromDb == null)
        {
            return null!;
        }

        List<CategoryReadDto> categoriesDto = categoriesFromDb.Select(c => new CategoryReadDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ParentCategoryId = c.ParentCategoryId,
            Image = c.Image,
            ParentCategoryName = c.ParentCategoryId != null ? _unitOfWork.CategoriesRepo.GetById(Guid.Parse(c.ParentCategoryId.ToString())).Name : null,
            products = GetProductsByCategoryId(c.Id) ?? null!,
        }).ToList();

        return categoriesDto;
    }
    #endregion

    #region Get Products For One selected Category
    public List<ProductReadDto>? GetProductsByCategoryId(Guid categoryId)
    {
        List<Product>? productsFromDb = _unitOfWork.CategoriesRepo.GetProductsByCategoryId(categoryId).Products;

        if (productsFromDb == null)
        {
            return null;
        }

        List<ProductReadDto> productsDto = productsFromDb.Select(p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Discount = p.Discount,
            Rate = p.Rate,
        }).ToList();

        return productsDto;
    }
    #endregion

    #region Add Category
    public bool AddCategory(CategoryAddDto addCategoryDto)
    {

        Category? newCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = addCategoryDto.Name,
            Description = addCategoryDto.Description,
            ParentCategoryId = addCategoryDto.ParentCategoryId ?? null,
            Image = addCategoryDto.Image,

        };

        _unitOfWork.CategoriesRepo.Add(newCategory);

        return _unitOfWork.SaveChange() > 0;
    }

    #endregion

    #region Delete Category

    public bool DeleteCategory(Guid CategoryId)
    {
        Category? categoryFromDb = _unitOfWork.CategoriesRepo.GetById(CategoryId);

        if (categoryFromDb != null)
        {
            _unitOfWork.CategoriesRepo.Delete(categoryFromDb);
        }
        int numberOfAffectedRows = _unitOfWork.SaveChange();
        return numberOfAffectedRows > 0;

    }
    #endregion

    #region Get Category To Update

    public CategoryUpdateDto? CategoryToUpdate(Guid CategoryId)
    {

        Category? categoryFromDb = _unitOfWork.CategoriesRepo.GetById(CategoryId);
        if (categoryFromDb != null)
        {

            return null;
        }

        return new CategoryUpdateDto
        {
            CategoryId = categoryFromDb.Id,
            Name = categoryFromDb.Name,
            Description = categoryFromDb.Description,
            ParentCategoryId = categoryFromDb.ParentCategoryId,
            Image = categoryFromDb.Image,
        };
    }

    #endregion

    #region Update Category


    public bool UpdateCategory(CategoryUpdateDto updateCategoryDto)
    {
        Category? categoryFromDb = _unitOfWork.CategoriesRepo.GetById(updateCategoryDto.CategoryId);
        if (categoryFromDb != null)
        {

            categoryFromDb.Name = updateCategoryDto.Name;
            categoryFromDb.Description = updateCategoryDto.Description;
            categoryFromDb.ParentCategoryId = updateCategoryDto.ParentCategoryId;
            categoryFromDb.Image = updateCategoryDto.Image;

        }
        _unitOfWork.CategoriesRepo.Update(categoryFromDb);

        return _unitOfWork.SaveChange() > 0;

    }
    #endregion

    #region Get Category By Id

    public CategoryReadDto? GetCategoryById(Guid CategoryId)
    {

        Category? categoryFromDb = _unitOfWork.CategoriesRepo.GetById(CategoryId);

        List<ProductReadDto> products = GetProductsByCategoryId(CategoryId);

        if (categoryFromDb is null)
        {
            return null;
        }
        return new CategoryReadDto
        {
            Id = categoryFromDb.Id,
            Name = categoryFromDb.Name,
            Description = categoryFromDb.Description,
            ParentCategoryId = categoryFromDb.ParentCategoryId,
            ParentCategoryName = categoryFromDb.ParentCategoryId != null ? _unitOfWork.CategoriesRepo.GetById(Guid.Parse(categoryFromDb.ParentCategoryId.ToString())).Name : null,
            products = products ?? null!,
            Image = categoryFromDb.Image

        };
    }



    #endregion

    #region Get SubCategories For Parent Category

    public List<SubCategoryReadDto> GetSubCategories(Guid parentCategoryId)
    {

        var subCategoreis = _unitOfWork.CategoriesRepo.GetSubCategories(parentCategoryId)
            .Select(c => new SubCategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Image = c.Image,
                products = c.Products.Select(p => new ProductDetailsReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Discount = p.Discount,
                    Rate = p.Rate,
                    CategoryId=c.Id,
                    ProductImages = p.ProductImages.Select(c => new ProductImageDto
                    {

                        ImageURL = c.ImageURL

                    }).ToList(),
                    ProductInfo = p.Product_Color_Size_Quantity.Select(i => new ProductInfoDto
                    {
                        Color = i.Color.ToString(),
                        Size = i.Size.ToString(),
                        Quantity = i.Quantity

                    }).ToList()


                }).ToList()



            }).ToList();

        return subCategoreis;
    }
    #endregion

    #region get Parent Categories


    public List<CategoryReadDto> GetParentCategory()
    {

        var ParentCategoriesFromDB = _unitOfWork.CategoriesRepo.GetParentCategory();


 
        var ParentCategoriesDto = ParentCategoriesFromDB.Select(c => new CategoryReadDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Image = c.Image,
            ParentCategoryId = c.ParentCategoryId,
            ParentCategoryName = c.ParentCategoryId != null ? _unitOfWork.CategoriesRepo.GetById(Guid.Parse(c.ParentCategoryId.ToString())).Name : null,
            products = GetProductsByCategoryId(c.Id) ?? null!,
        }).ToList();

        return ParentCategoriesDto;


    }
    #endregion


    #region Products by name



    public List<ProductReadDto>? GetProductsByName(string ProductName)
    {

        List<Product>? productsFromDb = _unitOfWork.CategoriesRepo.GetProductsByName(ProductName);

        if (productsFromDb == null)
        {
            return null;
        }

        List<ProductReadDto> productsDto = productsFromDb.Select(p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Discount = p.Discount,
            Rate = p.Rate,
        }).ToList();

        return productsDto;


    }

    #endregion



    #region get products by category



    public List<ProductDetailsReadDto>? GetProductsByCategoryIds(Guid categoryId)
    {
        Category? productsFromDb = _unitOfWork.CategoriesRepo.GetProductsByCategoryId(categoryId);

        if (productsFromDb == null)
        {
            return null;
        }

        List<ProductDetailsReadDto> productsDto = productsFromDb.Products.Select(p => new ProductDetailsReadDto
        {
            Id = p.Id,
            CategoryId = categoryId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Discount = p.Discount,
            Rate = p.Rate,
            ProductImages = p.ProductImages.Select(c => new ProductImageDto
            {

                ImageURL = c.ImageURL

            }).ToList(),
            ProductInfo = p.Product_Color_Size_Quantity.Select(i=>new ProductInfoDto
            {
                Color= i.Color.ToString(),
                Size=i.Size.ToString(),
                Quantity = i.Quantity

            }).ToList()
            
        }).ToList();

        return productsDto;
    }


    #endregion


    #region get products by parent category id

    public List<ProductDetailsReadDto>? GetProductsByParentCategoryIds(Guid categoryId) {


        List<Product>? productsFromDb = _unitOfWork.CategoriesRepo.GetProductsByParentCategory(categoryId);



        if (productsFromDb == null)
        {
            return null;
        }

        List<ProductDetailsReadDto> productsDto = productsFromDb.Select(p => new ProductDetailsReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Discount = p.Discount,
            Rate = p.Rate,
            ProductImages = p.ProductImages.Select(c => new ProductImageDto
            {

                ImageURL = c.ImageURL

            }).ToList()
        }).ToList();

        return productsDto;
    }


    #endregion
    public List<CategoryReadDto> GetCategoriesUnique()
    {
        var cat = _unitOfWork.CategoriesRepo.GetCategoriesUnique();
        List<CategoryReadDto> catDto = new List<CategoryReadDto> ();

        foreach(var c in cat) 
        {
            catDto.Add(new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Image = c.Image,
            });
        }

        return catDto;
    }

}
