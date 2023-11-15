using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public required string ProductName {  get; set; }

        public required string ProductDescription {  get; set; }

        public required int ProductPrice { get; set; }

        public int CustomerId {  get; set; }

        public string? ProductImage { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }


    public class ProductCategory
    {
        public int Id { get; set; }

        public required string ProductName { get; set; }

        public required string ProductDescription { get; set; }

        public required int ProductPrice { get; set; }

        public string? ProductImage { get; set; }

        public int CustomerId { get; set; }

        public  required List<Category> Categories { get ; set; }
    }
}
