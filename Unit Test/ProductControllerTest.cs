using EcommerceAPI.Controllers;
using EcommerceAPI.Data;
using EcommerceAPI.Interfaces.Service;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Unit_Test.TestFiles;

namespace Unit_Test
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Check_For_Correct_Return_Type()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.GetProducts())
                .Returns(TestModels.GetTestProducts());

            var controller = new ProductController(mockRepo.Object);

            var result = controller.GetProducts();

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(3, ((List<Product>)((ObjectResult)result).Value!).Count);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_When_No_Products()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.GetProducts())
                .Returns(new List<Product>());

            var controller = new ProductController(mockRepo.Object);

            var result = controller.GetProducts();

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(0, ((List<Product>)((ObjectResult)result).Value!).Count);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_When_Product_Not_Found()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.GetProductById(1))
                .Returns((Product)null);

            var controller = new ProductController(mockRepo.Object);

            var result = controller.GetProduct(1);

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(404, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_When_Product_Found()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.GetProductById(1))
                .Returns(TestModels.GetTestProducts().First());

            var controller = new ProductController(mockRepo.Object);

            var result = controller.GetProduct(1);

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(200, ((ObjectResult)result).StatusCode);

            Assert.AreEqual(1, ((Product)((ObjectResult)result).Value!).Id);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_When_Product_Added()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.AddProduct(It.IsAny<ProductCategory>()))
                .Returns(TestModels.GetTestProducts().Last());

            var controller = new ProductController(mockRepo.Object);

            var result = controller.AddProduct(TestModels.GetProductTestCategory());

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(201, ((ObjectResult)result).StatusCode);

            Console.WriteLine(((ObjectResult)result).Value);
            Assert.ReferenceEquals(new { message = "Product has been created" }, ((ObjectResult)result).Value);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_When_Product_Not_Added()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.AddProduct(It.IsAny<ProductCategory>()))
                .Throws(new Exception("Internal Server Error"));

            var controller = new ProductController(mockRepo.Object);

            var result = controller.AddProduct(TestModels.GetProductTestCategory());

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_Delete_Product_Not_Found()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.DeleteProductById(10))
                .Throws(new CustomErrorException("Product not found"));

            var controller = new ProductController(mockRepo.Object);

            var result = controller.DeleteProduct("10");

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(404, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_Delete_Product_Found()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.DeleteProductById(1));

            var controller = new ProductController(mockRepo.Object);

            var result = controller.DeleteProduct("1");

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(204, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_Delete_Product_Exception()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.DeleteProductById(1))
                .Throws(new Exception("Internal Server Error"));

            var controller = new ProductController(mockRepo.Object);

            var result = controller.DeleteProduct("1");

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_Update_Product_Not_Found()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.UpdateProduct(10, It.IsAny<Product>()))
                .Throws(new CustomErrorException("Product not found"));

            var controller = new ProductController(mockRepo.Object);

            var result = controller.UpdateProduct(TestModels.GetTestProducts().First(), 10);

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(404, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void Check_For_Correct_Return_Type_Update_Product_Found()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.UpdateProduct(1, It.IsAny<Product>()));

            var controller = new ProductController(mockRepo.Object);

            var result = controller.UpdateProduct(TestModels.GetTestProducts().First(), 1);

            Assert.IsInstanceOfType(result, typeof(ActionResult));

            Assert.AreEqual(200, ((ObjectResult)result).StatusCode);
        }

    }
}