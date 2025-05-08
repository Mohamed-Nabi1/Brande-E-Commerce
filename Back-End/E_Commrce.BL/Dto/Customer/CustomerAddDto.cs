using E_Commerce.DAL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.BL
{
    public class CustomerAddDto
    {
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
        public Countries Country { get; set; }

        public string Password { get; set; } = string.Empty;
    }
}
