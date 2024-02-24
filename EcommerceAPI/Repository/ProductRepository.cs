using EcommerceAPI.Data;
using EcommerceAPI.Interfaces.Respository;
using EcommerceAPI.Models;

namespace EcommerceAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetProducts()
        {
            using var _context = new EcommerceContext();
            return _context.Products.Select(product => new Product
            {
                Id = product.Id,
                ProductPrice = product.ProductPrice,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductImage = product.ProductImage,
                CustomerId = product.CustomerId,

                Categories = product.Categories.Select(category => new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Type = category.Type,
                }).ToList()
            }).ToList();
        }

        public Product AddProduct(ProductCategory product)
        {
            using var _context = new EcommerceContext();

            if (product == null)
            {
                throw new Exception("Product is null");
            }

            Product ProductData = new()
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductImage = product.ProductImage,
                CustomerId = product.CustomerId,
            };
            foreach (var category in product.Categories)
            {
                var existingCategory = _context.Categories.FirstOrDefault(x => x.Name == category.Name);

                Console.WriteLine($"  ExistingCategory: {existingCategory}");

                if (existingCategory != null)
                {
                    ProductData.Categories.Add(existingCategory);

                }
            }
            _context.Products.Add(ProductData);
            _context.SaveChanges();
            return ProductData;
        }

        public Product GetProductById(int id)
        {
            using var _context = new EcommerceContext();
            return _context.Products.Where(product => product.Id == id).Select(product => new Product
            {
                Id = product.Id,
                ProductPrice = product.ProductPrice,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductImage = product.ProductImage,
                CustomerId = product.CustomerId,

                Categories = product.Categories.Select(category => new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Type = category.Type,
                }).ToList()
            }).FirstOrDefault();
        }

        public void DeleteProductById(int id)
        {
            using var _context = new EcommerceContext();
            var product = _context.Products.FirstOrDefault(product => product.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            else
            {
                throw new CustomErrorException("Product not found");
            }
        }

        public void UpdateProduct(int id, Product product)
        {
            using var _context = new EcommerceContext();
            var productData = _context.Products.FirstOrDefault(product => product.Id == id);
            if (productData != null)
            {
                productData.ProductName = product.ProductName;
                productData.ProductDescription = product.ProductDescription;
                productData.ProductPrice = product.ProductPrice;
                productData.ProductImage = product.ProductImage;
                productData.CustomerId = product.CustomerId;
                _context.SaveChanges();
            }
            else
            {
                throw new CustomErrorException("Product not found");
            }
        }
    }
}
