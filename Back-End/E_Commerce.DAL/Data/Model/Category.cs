namespace E_Commerce.DAL;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    //Self_Relation
    public Guid? ParentCategoryId { get; set; } = Guid.Empty;
    public Category? ParentCategory { get; set; }

    public List<Category>? SubCategories { get; set; } = new List<Category>();

    public List<Product> Products { get; set; } = new List<Product>();


}

