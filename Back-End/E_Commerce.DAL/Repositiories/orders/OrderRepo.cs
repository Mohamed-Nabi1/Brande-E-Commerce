using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DAL
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        private readonly E_CommerceContext _context;
        public OrderRepo(E_CommerceContext context) : base(context)
        {
            _context=context;
        }

        public Customer? GetCustomerByOrderId(Guid id)
        {
            return _context.Set<Order>().Where(o => o.Id == id).Select(o => o.Customer).FirstOrDefault();
        }

        public Order? GetOrderDetails(Guid id)  
        {
            return _context.Set<Order>().Include(o => o.OrderProducts).ThenInclude(o => o.Product)
                                        .FirstOrDefault(o => o.Id == id);
        }

        public Order? GetOrderProducts(Guid id)
        {
            return _context.Set<Order>().Include(o=>o.OrderProducts).ThenInclude(o => o.Product)
                                        .FirstOrDefault(o => o.Id == id);
        }
        public List<Order> GetOrdersWithCustomer()
        {
            return _context.Set<Order>().Include(o => o.Customer).ToList();
        }

        public Order? GetOrderProductsAndCustomer(Guid id)
        {
            return _context.Set<Order>().Include(o => o.OrderProducts).ThenInclude(op => op.Product)
                                        .Include(o => o.Customer).Where(o => o.Id == id)
                                        .FirstOrDefault();
        }
        public void DeleteRangeOfOrderProduct(List<OrderProduct> orderProductsToDelete)
        {
            _context.OrderProducts.RemoveRange(orderProductsToDelete);
        }

        public void AddOrderProductsRange(List<OrderProduct> orderProductsToAdd)
        {
            _context.OrderProducts.AddRange(orderProductsToAdd);
        }

        public void DeleteFromOrderProductsByProductId(Guid id)
        {
            _context.OrderProducts
                 .Where(ip => ip.ProductId == id)
                 .ExecuteDelete();
        }

        public List<Order> GetOrdersByCustomerId(string customerId)
        {
            return _context.Set<Order>()
        .Include(o => o.OrderProducts)
        .ThenInclude(op => op.Product)
        .Where(o => o.CustomerId == customerId)
        .ToList();
        }
    }
}
