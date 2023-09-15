using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Customer
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public List<Product> Products { get; } = new List<Product>();
    }
}
