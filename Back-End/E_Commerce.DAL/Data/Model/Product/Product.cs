using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DAL;

public class Product
{
    public Guid Id { get; set; }
    [MinLength(3)]
    [MaxLength(30)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    [Range(0, 1)]
    public decimal Discount { get; set; } = 0;
    [Range(0, 5)]
    public decimal Rate { get; set; } = 0;
    public List<Product_IMG> ProductImages { get; set; } = new List<Product_IMG>();
    public List<ProductColorSizeQuantity> Product_Color_Size_Quantity { get; set; } = new List<ProductColorSizeQuantity>();
    public List<CustomerReview>? Reviews { get; set; }
    public List<CartProduct>? Carts { get; set; }
    public List<Category> Categories { get; set; } = new List<Category>();
    public List<WishList>? WishLists { get; set; }
    public List<OrderProduct> ProductOrders { get; set; } = new List<OrderProduct>();

}
