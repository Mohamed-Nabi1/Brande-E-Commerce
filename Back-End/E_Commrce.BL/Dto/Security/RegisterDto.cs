using E_Commerce.DAL;

namespace E_Commerce.BL;

public class RegisterDto
{
    public string FirstName { get; set; } = null!;
    public string MidName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public Countries Country { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

}
