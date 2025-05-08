

namespace E_Commerce.DAL
{
    public interface IUnitOfWork 
    {

        public ICustomerRepo CustomerRepo { get;  }
        public ICartRepo CartRepo { get;  }
        public ICategoriesRepo CategoriesRepo { get; }
        public ICustomerReviewRepo CustomerReviewRepo { get; }
        public IOrderRepo OrderRepo { get; }
        public IProductsRepo ProductsRepo { get;  }
        public IUsersRepo UsersRepo { get;  }
        public IWishListRepo WishListRepo { get; }
        public ICartProductRepo CartProductRepo { get; }

        int SaveChange();
    }
}
