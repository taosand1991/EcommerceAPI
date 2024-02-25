using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Integration_Test.External_Service;
using Moq;
using Newtonsoft.Json;
using Unit_Test.TestFiles;

namespace Integration_Test.Tests
{
    [TestClass]
    public class ProductIntegrationTest : CustomBaseTest
    {
        [TestMethod]
        public async Task TestGetAllProducts()
        {
            ExternalMockService.ProductService
                .Setup(x => x.GetProducts())
                .Returns(TestModels.GetTestProducts());

            var client = GetClient();
            var response = await client.GetAsync("/api/product");
            var responseMessage = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<Product>>(responseMessage);
            Assert.IsNotNull(products);
            Assert.AreEqual(3, products.Count);
        }

        [TestMethod]
        public async Task TestGetProductById()
        {
            ExternalMockService.ProductService
                .Setup(x => x.GetProductById(1))
                .Returns(TestModels.GetTestProducts().First());

            var client = GetClient();
            var response = await client.GetAsync("/api/product/1");
            var responseMessage = await response.Content.ReadAsStringAsync();

            var product = JsonConvert.DeserializeObject<Product>(responseMessage);
            Assert.IsNotNull(product);
            Assert.AreEqual(1, product.Id);
        }

        [TestMethod]
        public async Task TestGetProductById_NotFound()
        {
            ExternalMockService.ProductService
                .Setup(x => x.GetProductById(10))
                .Returns((Product)null);

            var client = GetClient();
            var response = await client.GetAsync("/api/product/1");

            Assert.AreEqual(404, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task TestAddProduct()
        {
            ExternalMockService.ProductService
                .Setup(x => x.AddProduct(It.IsAny<ProductCategory>()))
                .Returns(TestModels.GetTestProducts().First());

            var client = GetClient();
            var response = await client.PostAsJsonAsync("/api/product", TestModels.GetTestProducts().First());

            Assert.AreEqual(201, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task TestAddProduct_InternalServerError()
        {
            ExternalMockService.ProductService
                .Setup(x => x.AddProduct(It.IsAny<ProductCategory>()))
                .Throws(new Exception("Internal Server Error"));

            var client = GetClient();
            var response = await client.PostAsJsonAsync("/api/product", TestModels.GetTestProducts().First());

            Assert.AreEqual(500, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteProduct()
        {
            ExternalMockService.ProductService
                .Setup(x => x.DeleteProductById(1));

            var client = GetClient();
            var response = await client.DeleteAsync("/api/product/1");

            Assert.AreEqual(204, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteProduct_NotFound()
        {
            ExternalMockService.ProductService
                .Setup(x => x.DeleteProductById(10))
                .Throws(new CustomErrorException("Product not found"));

            var client = GetClient();
            var response = await client.DeleteAsync("/api/product/10");

            Assert.AreEqual(404, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteProduct_InternalServerError()
        {
            ExternalMockService.ProductService
                .Setup(x => x.DeleteProductById(1))
                .Throws(new Exception("Internal Server Error"));

            var client = GetClient();
            var response = await client.DeleteAsync("/api/product/1");

            Assert.AreEqual(500, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task TestUpdateProduct()
        {
            ExternalMockService.ProductService
                .Setup(x => x.UpdateProduct(1, It.IsAny<Product>()));

            var client = GetClient();
            var response = await client.PutAsJsonAsync("/api/product/1", TestModels.GetTestProducts().First());

            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task TestUpdateProduct_NotFound()
        {
            ExternalMockService.ProductService
                .Setup(x => x.UpdateProduct(10, It.IsAny<Product>()))
                .Throws(new CustomErrorException("Product not found"));

            var client = GetClient();
            var response = await client.PutAsJsonAsync("/api/product/10", TestModels.GetTestProducts().First());

            Assert.AreEqual(404, (int)response.StatusCode);
        }

        [TestMethod]
        public async Task TestUpdateProduct_InternalServerError()
        {
            ExternalMockService.ProductService
                .Setup(x => x.UpdateProduct(1, It.IsAny<Product>()))
                .Throws(new Exception("Internal Server Error"));

            var client = GetClient();
            var response = await client.PutAsJsonAsync("/api/product/1", TestModels.GetTestProducts().First());

            Assert.AreEqual(500, (int)response.StatusCode);
        }
    }
}
