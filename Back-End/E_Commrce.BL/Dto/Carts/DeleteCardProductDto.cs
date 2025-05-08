
namespace E_Commerce.BL
{
    public class DeleteCardProductDto
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }



    }
}
