
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL
{
    public class CartsProductRepo : GenericRepo<CartProduct>, ICartProductRepo
    {
        private readonly E_CommerceContext _context;

        public CartsProductRepo(E_CommerceContext context) : base(context)
        {
            _context = context;
        }

        public CartProduct? GetById(Guid productId, Guid cartId)
        {
           return _context.CartProduct.FirstOrDefault(p => p.ProductId == productId && p.CartId == cartId);
            
        }


        public CartProduct? GetCartProductByCustomerId(Guid CustomerId)
        {
            return _context.Set<CartProduct>()
                .Include(p=>p.Product).ThenInclude(p=>p.Product_Color_Size_Quantity)
                .Include(c=>c.Cart)
                .FirstOrDefault(cp => cp.Cart.CustomerId == CustomerId);
        }
    }
}
