using Azure;
using E_Commerce.DAL;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;

namespace E_Commerce.BL;

public class ProductManager : IProductManager
{
    private readonly IUnitOfWork unitOfWork;

    public ProductManager(IUnitOfWork _unitOfWork)
    {
        unitOfWork = _unitOfWork;
    }

    public ProductPaginationDto AllProductsPagination(int page, int countPerPage)
    {
        var products = unitOfWork.ProductsRepo.GetAllProductsPagination(page, countPerPage);

        var subcategories = products.SelectMany(p => p.Categories).Distinct().ToList();

        var items = new List<ProductCategoryPagination>();

        foreach (var subcategory in subcategories)
        {
            var subcategoryProducts = products.Where(p => p.Categories.Contains(subcategory));

            var productCount = subcategoryProducts.Count();


            var subcategoryDto = new ProductCategoryPagination
            {
                CategoryName = subcategory.Name,
                Count = productCount,
                Products = subcategoryProducts.Select(p => new ProductDetailsReadDto
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
                    }).ToList(),
                }).ToList()
            };

            items.Add(subcategoryDto);
        }

        var totalCount = items.Sum(item => item.Count);
        return new ProductPaginationDto
        {
            totalCount = totalCount,
            items = items
        };
    }

    List<ProductWithImagesDto> GetProductsUnique()
    {
        var products = unitOfWork.ProductsRepo.GetProductsByCategoryUnique();
        List<ProductWithImagesDto> productsDto = new List<ProductWithImagesDto>();
        foreach (var p in products)
        {
            productsDto.Add(new ProductWithImagesDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Discount = p.Discount,
                Rate = p.Rate,
                ProductImages = p.ProductImages.Select(i => i.ImageURL).ToList(),
                Review = unitOfWork.ProductsRepo.GetProductReviews(p.Id).Reviews.Count
            });
        }
        return productsDto;
    }

    public List<ProductReadDto> AllProducts()
    {
        List<Product> productsFromDB = unitOfWork.ProductsRepo.GetAll();
        return productsFromDB.Select(p => new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Discount = p.Discount,
            Rate = p.Rate,
        }).ToList();
    }

    public bool Add(ProductAddDto productAdd)
    {
        if (productAdd is null)
        {
            return false;
        }

        Product newProduct = new Product()
        {
            Id = Guid.NewGuid(),
            Name = productAdd.Name,
            Description = productAdd.Description,
            Price = productAdd.Price,
            Discount = productAdd.Discount,
        };

        //Add Images
        newProduct.ProductImages = productAdd.Images.Select(img => new Product_IMG
        {
            ProductID = newProduct.Id,
            ImageURL = img,
        }).ToList();

        for (int i = 0; i < newProduct.ProductImages.Count; i++)
        {
            for (int j = 1; j < newProduct.ProductImages.Count; j++)
            {
                if (newProduct.ProductImages[i].ImageURL == newProduct.ProductImages[j].ImageURL
                    && i != j)
                {
                    return false;
                }
            }
        }

        //Add Info
        newProduct.Product_Color_Size_Quantity = productAdd.ProductInfo.Select(pi => new ProductColorSizeQuantity
        {
            ProductID = newProduct.Id,
            Color = (Color)Enum.Parse(typeof(Color), pi.Color),
            Size = (Size)Enum.Parse(typeof(Size), pi.Size),
            Quantity = pi.Quantity,
        }).ToList();

        for (int i = 0; i < newProduct.Product_Color_Size_Quantity.Count; i++)
        {
            for (int y = 1; y < newProduct.Product_Color_Size_Quantity.Count; y++)
            {
                if (newProduct.Product_Color_Size_Quantity[i].Color == newProduct.Product_Color_Size_Quantity[y].Color
                    && newProduct.Product_Color_Size_Quantity[i].Size == newProduct.Product_Color_Size_Quantity[y].Size
                    && i != y)
                {
                    return false;
                }
            }
        }
        //Add Category
        for (int i = 0; i < productAdd.ProductCategories.Count; i++)
        {
            var cat = unitOfWork.CategoriesRepo.GetById(productAdd.ProductCategories[i]);
            if (cat == null)
            {
                return false;
            }
            newProduct.Categories.Add(cat);
        }


        unitOfWork.ProductsRepo.Add(newProduct);

        return unitOfWork.SaveChange() > 0;
    }

    public bool Delete(Guid productId)
    {
        Product? productFromDB = unitOfWork.ProductsRepo.GetById(productId);
        if (productFromDB == null)
        {
            return false;
        }

        unitOfWork.ProductsRepo.Delete(productFromDB);
        return unitOfWork.SaveChange() > 0;
    }

    public ProductDetailsReadDto? ProductDetails(Guid productId)
    {
        Product? productFromDB = unitOfWork.ProductsRepo.GetProductDetails(productId);
        if (productFromDB == null)
        {
            return null;
        }

        return new ProductDetailsReadDto
        {
            Id = productId,
            Name = productFromDB.Name,
            Description = productFromDB.Description,
            Rate = productFromDB.Rate,
            Price = productFromDB.Price,
            Discount = productFromDB.Discount,
            ProductImages = productFromDB.ProductImages.Select(img => new ProductImageDto
            {
                ImageURL = img.ImageURL,
            }).ToList(),

            ProductInfo = productFromDB.Product_Color_Size_Quantity.Select(inf => new ProductInfoDto
            {
                Color = inf.Color.ToString(),
                Size = inf.Size.ToString(),
                Quantity = inf.Quantity,
            }).ToList()
        };
    }

    public List<ProductReviewsDto>? ProductReviews(Guid productId)
    {
        Product? productFromDB = unitOfWork.ProductsRepo.GetProductReviews(productId);
        if (productFromDB == null)
        {
            return null;
        }

        return productFromDB.Reviews!.Select(r => new ProductReviewsDto
        {
            CustomerName = r.Customer.FirstName + " " + r.Customer.LastName,
            Description = r.Description,
            Rate = r.Rate,
            CreatedTime = r.CreatedTime,
        }).ToList();
    }

    public List<ProductCategories>? ProductCategories(Guid productId)
    {
        Product? productFromDB = unitOfWork.ProductsRepo.GetProductCategories(productId);
        if (productFromDB == null)
        {
            return null;
        }

        return productFromDB.Categories.Select(c => new ProductCategories
        {
            Id = c.Id,
            Name = c.Name,
        }).ToList();
    }

    public ProductUpdateDto? ProductToUpdate(Guid productId)
    {
        Product? productFromDB = unitOfWork.ProductsRepo.GetProductToUpdate(productId);
        if (productFromDB == null)
        {
            return null;
        }

        return new ProductUpdateDto
        {
            Id = productFromDB.Id,
            Name = productFromDB.Name,
            Description = productFromDB.Description,
            Rate = productFromDB.Rate,
            Price = productFromDB.Price,
            Discount = productFromDB.Discount,
            ProductCategories = productFromDB.Categories.Select(c => c.Id).ToList(),
            Images = productFromDB.ProductImages.Select(c => c.ImageURL).ToList(),

            ProductInfo = productFromDB.Product_Color_Size_Quantity.Select(pi => new ProductInfoDto
            {
                Size = pi.Size.ToString(),
                Color = pi.Color.ToString(),
                Quantity = pi.Quantity,
            }).ToList()
        };
    }

    public bool Update(ProductUpdateDto productUpdate)
    {
        Product? productFromDB = unitOfWork.ProductsRepo.GetProductToUpdate(productUpdate.Id);
        if (productFromDB == null)
        {
            return false;
        }
        productFromDB.Name = productUpdate.Name;
        productFromDB.Description = productUpdate.Description;
        productFromDB.Rate = productUpdate.Rate;
        productFromDB.Price = productUpdate.Price;
        productFromDB.Discount = productUpdate.Discount;

        //Update Images

        for (int i = 0; i < productUpdate.Images.Count; i++)
        {
            for (int j = 1; j < productUpdate.Images.Count; j++)
            {
                if (productUpdate.Images[i] == productUpdate.Images[j]
                    && i != j)
                {
                    return false;
                }
            }
        }

        productFromDB.ProductImages = productUpdate.Images.Select(i => new Product_IMG
        {
            ProductID = productFromDB.Id,
            ImageURL = i,
        }).ToList();

        //Update Info

        for (int i = 0; i < productUpdate.ProductInfo.Count; i++)
        {
            for (int y = 1; y < productUpdate.ProductInfo.Count; y++)
            {
                if (productUpdate.ProductInfo[i].Color == productUpdate.ProductInfo[y].Color
                    && productUpdate.ProductInfo[i].Size == productUpdate.ProductInfo[y].Size
                    && i != y)
                {
                    return false;
                }
            }
        }

        productFromDB.Product_Color_Size_Quantity = productUpdate.ProductInfo.Select(pi => new ProductColorSizeQuantity
        {
            Quantity = pi.Quantity,
            Color = (Color)Enum.Parse(typeof(Color), pi.Color),
            Size = (Size)Enum.Parse(typeof(Size), pi.Size)
        }).ToList();

        //Update Info
        productFromDB.Categories.Clear();

        for (int i = 0; i < productUpdate.ProductCategories.Count; i++)
        {
            var cat = unitOfWork.CategoriesRepo.GetById(productUpdate.ProductCategories[i]);
            if (cat == null)
            {
                return false;
            }
            productFromDB.Categories.Add(cat);
        }

        return unitOfWork.SaveChange() > 0;
    }

    public ProductAfterFillterByColor ProductFillterByColor(ProductFillterByColor productDto)
    {
        Product? product = unitOfWork.ProductsRepo.GetProductDetails(productDto.Id);
        var filtered = new ProductAfterFillterByColor
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Discount = product.Discount,
            Rate = product.Rate,
            ProductImages = product.ProductImages.Select(i => new ProductImageDto
            {
                ImageURL = i.ImageURL
            }).ToList(),
            ProductInfo = product.Product_Color_Size_Quantity.Where(p => p.Color == (Color)Enum.Parse(typeof(Color), productDto.Color)).Select(p => new ProductInfoDto
            {
                Color = p.Color.ToString(),
                Size = p.Size.ToString(),
                Quantity = p.Quantity

            }).ToList()


        };

        return filtered;
    }

    public List<ProductWithImagesDto> ProductsWithImages()
    {

        var products = unitOfWork.ProductsRepo.GetProductsWithImages();
        List<ProductWithImagesDto> productImages = products.Select(p => new ProductWithImagesDto
        {
            Id = p.Id,
            Rate = p.Rate,
            ProductImages = p.ProductImages.Select(i => i.ImageURL).ToList(),
            Price = p.Price,
            Discount = p.Discount,
            Description = p.Description,
            Name = p.Name,
            Review = unitOfWork.ProductsRepo.GetProductReviews(p.Id)!.Reviews!.Count
        }).ToList();

        return productImages;
    }

    public ProductDetailsDistinctDto? ProductDetailsDistinct(Guid productId)
    {
        Product? productFromDB = unitOfWork.ProductsRepo.GetProductDetails(productId);
        if (productFromDB == null)
        {
            return null;
        }

        return new ProductDetailsDistinctDto
        {
            Id = productId,
            Name = productFromDB.Name,
            Description = productFromDB.Description,
            Rate = productFromDB.Rate,
            Price = productFromDB.Price,
            Discount = productFromDB.Discount,
            ProductImages = productFromDB.ProductImages.Select(img => new ProductImageDto
            {
                ImageURL = img.ImageURL,
            }).ToList(),


            ProductInfo = productFromDB.Product_Color_Size_Quantity
            .Select(i => new ProductInfoColorDistinctDto
            {
                Color = i.Color.ToString(),
                SizeQuantities = productFromDB.Product_Color_Size_Quantity.Where(c => c.Color == i.Color).Select(p => new ProductSizeQuantityDto
                {
                    Quantity = p.Quantity,
                    Size = p.Size.ToString(),
                }).ToList(),

            }).Distinct(new ProductInfoColorDistinctDtoEqualityComparer()).ToList(),
        };
    }

    List<ProductWithImagesDto> IProductManager.GetProductsUnique()
    {
        var products = unitOfWork.ProductsRepo.GetProductsWithImages();
        List<ProductWithImagesDto> productImages = products.Select(p => new ProductWithImagesDto
        {
            Id = p.Id,
            Rate = p.Rate,
            ProductImages = p.ProductImages.Select(i => i.ImageURL).ToList(),
            Price = p.Price,
            Discount = p.Discount,
            Description = p.Description,
            Name = p.Name,
            Review = unitOfWork.ProductsRepo.GetProductReviews(p.Id)!.Reviews!.Count
        }).ToList();

        return productImages;
    }

    public List<ProductWithImagesDto> ProductsStartsWith(string query)
    {
        var productsFromDB = unitOfWork.ProductsRepo.GetProductStartsWith(query);

        return productsFromDB.Select(p => new ProductWithImagesDto
        {
            Id = p.Id,
            Rate = p.Rate,
            ProductImages = p.ProductImages.Select(i => i.ImageURL).ToList(),
            Price = p.Price,
            Discount = p.Discount,
            Description = p.Description,
            Name = p.Name,
            Review = unitOfWork.ProductsRepo.GetProductReviews(p.Id)!.Reviews!.Count
        }).ToList();
    }
}