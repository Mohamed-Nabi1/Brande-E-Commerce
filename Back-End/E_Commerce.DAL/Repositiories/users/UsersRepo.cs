using Microsoft.EntityFrameworkCore;
using System.Data;
namespace E_Commerce.DAL;

public class UsersRepo : GenericRepo<User>, IUsersRepo
{

    private readonly E_CommerceContext _context;

    public UsersRepo(E_CommerceContext context) : base(context)
    {

        _context = context;

    }

    public List<User> GetAllUsersByRole(UserRole role)
    {
        return _context.Set<User>().Where(c => c.Role == role).ToList();
    }

    public Customer? GetUserByEmail(string email)
    {
        return _context.Set<Customer>()
            .FirstOrDefault(user => user.Email == email);
    }

    public Customer? GetUserByPhonNumber(string phoneNumber)
    {
        return _context.Set<Customer>()
          .FirstOrDefault(user => user.PhoneNumber == phoneNumber);
    }

    public List<User> GetAllUsersByCity(string city, UserRole role)
    {

        var usersByCity = _context.Set<Customer>()
                  .Where(customer => customer.City == city && customer.Role == role)
                        .ToList<User>();

        return usersByCity;
    }
}






