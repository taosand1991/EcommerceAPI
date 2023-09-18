using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
 
    public class Category
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        [DefaultValue("category")]
        public required string Type { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
