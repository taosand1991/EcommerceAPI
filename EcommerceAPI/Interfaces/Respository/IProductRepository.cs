using EcommerceAPI.Models;

namespace EcommerceAPI.Interfaces.Respository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product AddProduct(ProductCategory product);
        Product GetProductById(int id);
        void UpdateProduct(int id, Product product);
        void DeleteProductById(int id);
    }
}
