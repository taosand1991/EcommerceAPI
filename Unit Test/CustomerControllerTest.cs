using EcommerceAPI.Controllers;
using EcommerceAPI.Data;
using EcommerceAPI.Dto;
using EcommerceAPI.Interfaces.Service;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Unit_Test.TestFiles;

namespace Unit_Test
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public void GetCustomerById_WhenCalled_ReturnsOkResult()
        {
            var mockRepo = new Mock<ICustomerService>();

            mockRepo.Setup(repo => repo.GetCustomers())
                .Returns(TestModels.GetTestCustomers());

            var controller = new CustomerController(mockRepo.Object);

            var result = controller.GetCustomers();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(2, ((List<CustomerDto>)((ObjectResult)result).Value!).Count);
        }

        [TestMethod]
        public void GetCustomerById_WhenCalled_ReturnsNotFound()
        {
            var mockRepo = new Mock<ICustomerService>();

            mockRepo.Setup(repo => repo.GetCustomerById(1))
                .Throws(new CustomErrorException("Customer not found"));

            var controller = new CustomerController(mockRepo.Object);

            var result = controller.GetCustomer(1);

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(404, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void AddCustomer_WhenCalled_ReturnsOkResult()
        {
            var mockRepo = new Mock<ICustomerService>();

            mockRepo.Setup(repo => repo.AddCustomer(TestModels.GetTestCustomer()))
                .Returns(TestModels.GetTestCustomer());

            var controller = new CustomerController(mockRepo.Object);

            var result = controller.AddCustomer(TestModels.GetTestCustomer());

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(201, ((ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public void AddCustomer_WhenCalled_ReturnsInternalServerError()
        {
            var mockRepo = new Mock<ICustomerService>();

            mockRepo.Setup(repo => repo.AddCustomer(It.IsAny<Customer>()))
                .Throws(new Exception("Internal Server Error"));

            var controller = new CustomerController(mockRepo.Object);

            var result = controller.AddCustomer(TestModels.GetTestCustomer());

            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(500, ((ObjectResult)result).StatusCode);
        }
    }
}
