using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DAL;
public class User : IdentityUser
{

    [MinLength(3)]
    [MaxLength(30)]
    public string FirstName { get; set; } = string.Empty;

    [MinLength(1)]
    [MaxLength(30)]
    public string MidName { get; set; } = string.Empty;
    [MinLength(3)]
    [MaxLength(30)]
    public string LastName { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.User;

}

