namespace E_Commerce.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly E_CommerceContext _context;
        public ICartRepo CartRepo { get; }
        public ICategoriesRepo CategoriesRepo { get; }
        public ICustomerReviewRepo CustomerReviewRepo { get; }
        public ICustomerRepo CustomerRepo { get; }
        public IOrderRepo OrderRepo { get; }
        public IProductsRepo ProductsRepo { get; }
        public IUsersRepo UsersRepo { get; }
        public IWishListRepo WishListRepo { get; }
        public ICartProductRepo CartProductRepo { get; }

        public UnitOfWork(E_CommerceContext context , ICartRepo cartRepo,ICategoriesRepo categoriesRepo
                            ,ICustomerReviewRepo customerReviewRepo , ICustomerRepo customerRepo
                            ,IOrderRepo orderRepo ,IProductsRepo productsRepo , IUsersRepo usersRepo
                            ,IWishListRepo wishListRepo , ICartProductRepo cartProductRepo)
        {
            _context = context;
            CartRepo = cartRepo;
            CategoriesRepo = categoriesRepo;
            CustomerReviewRepo = customerReviewRepo;
            CustomerRepo = customerRepo;
            OrderRepo = orderRepo;
            ProductsRepo = productsRepo;
            UsersRepo = usersRepo;
            WishListRepo = wishListRepo;
            CartProductRepo = cartProductRepo;
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }
    }
}