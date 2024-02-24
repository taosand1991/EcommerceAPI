using EcommerceAPI.Models;

namespace EcommerceAPI.Dto
{
    public record CustomerDto()
    {
        public required int Id { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public bool? Admin { get; init; }
        public string? PhoneNumber { get; init; }
        public Address? Address { get; init; }
        public List<Product> Products { get; init; } = new List<Product>();

    }

}
