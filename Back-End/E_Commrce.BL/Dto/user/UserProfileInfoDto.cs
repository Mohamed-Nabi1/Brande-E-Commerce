using E_Commerce.DAL;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL;

public class UserProfileInfoDto
{

    [Required(ErrorMessage = "First Name is required")]
    [MinLength(3, ErrorMessage = "Name can't be less that three characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Middle Name is required")]
    public string MidName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required")]
    [MinLength(3, ErrorMessage = "Name can't be less that three characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email format is not valid it must be in the form name@example.com")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one digit.")]
    public string Password { get; set; } = string.Empty;

    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid phone number.")]
    [Required(ErrorMessage = "PhoneNumber is required")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Street is required")]
    public string Street { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = string.Empty;
    [Required(ErrorMessage = "Country is required")]
    public Countries Country { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(2, ErrorMessage = "Name on card must be at least 2 characters long.")]
    public string? NameOnCard { get; set; } = string.Empty;
    [Required(ErrorMessage = "City is required")]
    public decimal? CardNumber { get; set; }
    [Required(ErrorMessage = "City is required")]
    public DateTime? ExpireDate { get; set; }

    public UserRole Role { get; set; }



}
