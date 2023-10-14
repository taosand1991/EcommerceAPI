using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace EcommerceAPI.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Customer
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required Boolean Admin { get; set; } = false;

        [PasswordPropertyText]
        public required string Password { get; set; }

        public List<Product> Products { get; } = new List<Product>();
    }

    public class LoginData
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
