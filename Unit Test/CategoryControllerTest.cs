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
    public class CategoryControllerTest
    {
        [TestMethod]
        public void TestGetAllCategories()
        {
            var mockRepo = new Mock<ICategoryService>();
            mockRepo.Setup(repo => repo.GetAllCategories())
                .Returns(TestModels.GetTestCategories());
            var controller = new CategoryController(mockRepo.Object);
            var result = controller.GetCategories();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(3, ((List<Category>)((ObjectResult)result).Value!).Count);
        }

        [TestMethod]
        public void TestAddCategory()
        {
            var mockRepo = new Mock<ICategoryService>();
            mockRepo.Setup(repo => repo.AddCategory(It.IsAny<Category>()));
            var controller = new CategoryController(mockRepo.Object);
            var result = controller.CreateCategory(TestModels.GetTestCategories().First());

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(201, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void TestDeleteCategory()
        {
            var mockRepo = new Mock<ICategoryService>();
            mockRepo.Setup(repo => repo.DeleteCategoryByName(It.IsAny<string>()));
            var controller = new CategoryController(mockRepo.Object);
            var result = controller.DeleteCategory("Test");

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(204, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void TestDeleteCategoryNotFound()
        {
            var mockRepo = new Mock<ICategoryService>();
            mockRepo.Setup(repo => repo.DeleteCategoryByName(It.IsAny<string>()))
                .Throws(new CustomErrorException("Category not found!"));
            var controller = new CategoryController(mockRepo.Object);
            var result = controller.DeleteCategory("Test");

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(404, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void TestDeleteCategoryInternalServerError()
        {
            var mockRepo = new Mock<ICategoryService>();
            mockRepo.Setup(repo => repo.DeleteCategoryByName(It.IsAny<string>()))
                .Throws(new Exception("Internal Server Error"));
            var controller = new CategoryController(mockRepo.Object);
            var result = controller.DeleteCategory("Test");

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void TestAddCategoryInternalServerError()
        {
            var mockRepo = new Mock<ICategoryService>();
            mockRepo.Setup(repo => repo.AddCategory(It.IsAny<Category>()))
                .Throws(new Exception("Internal Server Error"));
            var controller = new CategoryController(mockRepo.Object);
            var result = controller.CreateCategory(TestModels.GetTestCategories().First());

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
        }
    }
}
