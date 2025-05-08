using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL;

public class ProductsRepo : GenericRepo<Product>, IProductsRepo
{
    private readonly E_CommerceContext _context;

    public ProductsRepo(E_CommerceContext context) : base(context)
    {
        _context = context;
    }
    public List<Product> GetAllProductsPagination(int page, int countPerPage)
    {

        return _context.Set<Product>().Include(a => a.ProductImages).OrderBy(c => c.Price)
            .Skip((page - 1) * countPerPage)
            .Take(countPerPage).ToList();
    }



    public Product? GetProductDetails(Guid id)
    {
        return _context.Set<Product>()
            .Include(p => p.ProductImages)
            .Include(p => p.Product_Color_Size_Quantity)
            .FirstOrDefault(p => p.Id == id);
    }

    public Product? GetProductReviews(Guid id)
    {
        return _context.Set<Product>()
            .Include(p => p.Reviews!)
            .ThenInclude(c => c.Customer)
            .FirstOrDefault(p => p.Id == id);
    }
    public Product? GetProductCategories(Guid id)
    {
        return _context.Set<Product>()
            .Include(p => p.Categories)
            .FirstOrDefault(p => p.Id == id);
    }



    public Product? GetProductToUpdate(Guid id)
    {
        return _context.Set<Product>()
            .Include(p => p.ProductImages)
            .Include(p => p.Product_Color_Size_Quantity)
            .Include(p => p.Categories)
            .FirstOrDefault(p => p.Id == id);
    }
    public int GetCount()
    {
        return _context.Set<Product>().Count();
    }



    public List<Product> GetProductsWithImages()
    {
        return _context.Set<Product>()
            .Include(p => p.ProductImages)
            .Include(p => p.Product_Color_Size_Quantity)
            .Take(8).ToList();
    }

    public List<Product> GetProductsByCategoryUnique()
    {
        return _context.Set<Product>()
            .GroupBy(p => p.Name)
            .Where(g => g.Count() == 1)
            .Select(g => g.First())
            .Take(8)
            .ToList();
    }

    public List<Product> GetAllProductsWithDetails()
    {
        return _context.Set<Product>()
            .Include(p => p.Categories)
            .Include(p => p.Product_Color_Size_Quantity)
            .ToList();
    }

    public List<ProductColorSizeQuantity> GetProductsInfo()
    {
        return _context.Set<ProductColorSizeQuantity>().ToList();
    }

    public List<Product> GetProductStartsWith(string query)
    {
        return _context.Set<Product>()
            .Include(p => p.ProductImages)
            .Include(p => p.Product_Color_Size_Quantity)
            .Where(p => p.Name.StartsWith(query))
            .ToList();
    }
}