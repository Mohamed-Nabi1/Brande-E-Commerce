using Microsoft.EntityFrameworkCore;


namespace E_Commerce.DAL
{
    [PrimaryKey(nameof(ProductId), nameof(CartId),nameof(Size),nameof(Color))]

    public class CartProduct
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        public int ProductCount { get; set; }

        public Color Color { get; set; }
        public Size Size { get; set; }


    }
}
