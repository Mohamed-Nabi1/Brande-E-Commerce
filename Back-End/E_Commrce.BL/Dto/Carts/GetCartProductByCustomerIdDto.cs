
using Azure.Core.Pipeline;
using E_Commerce.DAL;

namespace E_Commerce.BL
{
    public class GetCartProductByCustomerIdDto
    {
        public Guid CartId { get; set; }
        public List<ProductDto> Products { get; set; }
        public decimal TotalCost { get; set; }
    }
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }

        public string Color { get; set; }
        public string Size { get; set; }
        

        public int QuantityInStock { get; set; }

    }
}
