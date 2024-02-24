using EcommerceAPI.Data;
using EcommerceAPI.Interfaces.Respository;
using EcommerceAPI.Models;

namespace EcommerceAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public void AddCategory(Category category)
        {
            using var _context = new EcommerceContext();

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            using var _context = new EcommerceContext();
            return _context.Categories.Select(category => new Category
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type,

                Products = category.Products.Select(product => new Product
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                }).ToList()
            }).ToList();
        }

        public void DeleteCategoryByName(string name)
        {
            using var _context = new EcommerceContext();
            var category = _context.Categories.FirstOrDefault(x => x.Name == name);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            else
            {
                throw new CustomErrorException("Category not found!");
            }

        }
    }
}
