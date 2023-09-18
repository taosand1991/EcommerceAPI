namespace EcommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string ProductName {  get; set; }

        public required string ProductDescription {  get; set; }

        public required int ProductPrice { get; set; }

        public int CustomerId {  get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
