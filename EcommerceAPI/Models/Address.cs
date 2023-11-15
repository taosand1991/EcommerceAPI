namespace EcommerceAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        public required string StreetNo { get; set; }
        public required string StreetName { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; }
        public int CustomerId { get; set; }
        public required Customer Customer { get; set; }
    
    }
}
