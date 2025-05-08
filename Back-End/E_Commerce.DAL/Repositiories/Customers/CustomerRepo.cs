using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL
{
    public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
    {
        private readonly E_CommerceContext _context;

        public CustomerRepo(E_CommerceContext context) : base(context)
        {
            _context = context;
        }

        public Customer? GetOrdersByCustomerId(Guid Id)
        {
            return _context.Set<Customer>().Include(c => c.Orders).ThenInclude(c => c.OrderProducts)
                    .FirstOrDefault(c => new Guid(c.Id) == Id);
        }
        public Customer? GetCustomerCartByCustomerId(Guid Id)
        {
            return _context.Set<Customer>().Include(c => c.Cart).ThenInclude(c => c.Products)
                .FirstOrDefault(c => new Guid(c.Id) == Id);
        }


        public Customer? GetWishListByCustomerId(Guid Id)
        {
            return _context.Set<Customer>().Include(w => w.WishList)
                .ThenInclude(p => p.Products)
                .FirstOrDefault(c => new Guid(c.Id) == Id);
        }

        public Customer? GetById(string Id)
        {
            return _context.Set<Customer>().Find(Id);
        }
    }
}
