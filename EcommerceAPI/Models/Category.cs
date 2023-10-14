using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
 
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        [DefaultValue("category")]
        public required string Type { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
