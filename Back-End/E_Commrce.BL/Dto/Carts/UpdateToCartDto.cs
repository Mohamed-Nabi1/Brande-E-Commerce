
using E_Commerce.DAL;

namespace E_Commerce.BL
{
    public class UpdateToCartDto
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

    }
}
