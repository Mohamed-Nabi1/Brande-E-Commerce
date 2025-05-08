using E_Commerce.DAL;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.BL
{
    public class CustomerUpdateDto
    {
        //public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(30)]
        public string MidName { get; set; } = string.Empty;
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
