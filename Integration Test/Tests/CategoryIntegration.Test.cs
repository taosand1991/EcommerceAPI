using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Integration_Test.External_Service;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Unit_Test.TestFiles;

namespace Integration_Test.Tests
{
    [TestClass]
    public class CategoryIntegration : CustomBaseTest
    {
        [TestMethod]
        public async Task TestGetAllCategories()
        {
            ExternalMockService.CategoryService
                .Setup(repo => repo.GetAllCategories())
                .Returns(TestModels.GetTestCategories());

            var client = GetClient();

            var response = await client.GetAsync("/api/category");

            var responseString = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(responseString);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(3, categories.Count);
        }

        [TestMethod]
        public async Task TestAddCategory()
        {
            ExternalMockService.CategoryService
                .Setup(repo => repo.AddCategory(It.IsAny<Category>()));

            var client = GetClient();

            var category = TestModels.GetTestCategories().First();
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/category", content);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteCategory()
        {
            ExternalMockService.CategoryService
                .Setup(repo => repo.DeleteCategoryByName(It.IsAny<string>()));

            var client = GetClient();

            var response = await client.DeleteAsync("/api/category/Test");

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteCategoryNotFound()
        {
            ExternalMockService.CategoryService
                .Setup(repo => repo.DeleteCategoryByName(It.IsAny<string>()))
                .Throws(new CustomErrorException("Category not found!"));

            var client = GetClient();

            var response = await client.DeleteAsync("/api/category/Test");

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task TestDeleteCategoryInternalServerError()
        {
            ExternalMockService.CategoryService
                .Setup(repo => repo.DeleteCategoryByName(It.IsAny<string>()))
                .Throws(new Exception("Internal Server Error"));

            var client = GetClient();

            var response = await client.DeleteAsync("/api/category/Test");

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public async Task TestAddCategoryInternalServerError()
        {
            ExternalMockService.CategoryService
                .Setup(repo => repo.AddCategory(It.IsAny<Category>()))
                .Throws(new Exception("Internal Server Error"));

            var client = GetClient();

            var category = TestModels.GetTestCategories().First();
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/category", content);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public async Task TestGetAllCategoriesInternalServerError()
        {
            ExternalMockService.CategoryService
                .Setup(repo => repo.GetAllCategories())
                .Throws(new Exception("Internal Server Error"));

            var client = GetClient();

            var response = await client.GetAsync("/api/category");

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
