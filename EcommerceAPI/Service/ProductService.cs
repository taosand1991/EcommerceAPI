using EcommerceAPI.Interfaces.Respository;
using EcommerceAPI.Interfaces.Service;
using EcommerceAPI.Models;

namespace EcommerceAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public List<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }
        public Product AddProduct(ProductCategory product)
        {
            return _productRepository.AddProduct(product);
        }
        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }
        public void UpdateProduct(int id, Product product)
        {
            _productRepository.UpdateProduct(id, product);
        }
        public void DeleteProductById(int id)
        {
            _productRepository.DeleteProductById(id);
        }
    }
}
