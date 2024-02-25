using EcommerceAPI.Dto;
using EcommerceAPI.Models;
using Integration_Test.External_Service;
using Newtonsoft.Json;
using System.Text;
using Unit_Test.TestFiles;

namespace Integration_Test.Tests
{
    [TestClass]
    public class CustomerIntegration : CustomBaseTest
    {
        [TestMethod]
        public async Task TestGetAllCustomers()
        {
            ExternalMockService.CustomerService
                 .Setup(x => x.GetCustomers())
                 .Returns(TestModels.GetTestCustomers());

            var client = GetClient();
            var response = client.GetAsync("/api/customer");

            var result = await response.Result.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<CustomerDto>>(result);
            Assert.IsNotNull(customers);
            Assert.AreEqual(2, customers.Count);
        }

        [TestMethod]
        public async Task TestGetCustomerById()
        {
            ExternalMockService.CustomerService
                 .Setup(x => x.GetCustomerById(1))
                 .Returns(TestModels.GetTestCustomers().First());

            var client = GetClient();
            var response = client.GetAsync("/api/customer/1");

            var result = await response.Result.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerDto>(result);
            Assert.IsNotNull(customer);
            Assert.AreEqual("John", customer.FirstName);
        }

        [TestMethod]
        public async Task TestAddCustomer()
        {
            var customer = TestModels.GetTestCustomer();
            ExternalMockService.CustomerService
                 .Setup(x => x.AddCustomer(customer))
                 .Returns(customer);

            var client = GetClient();
            var response = client.PostAsync("/api/customer", new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json"));

            var result = await response.Result.Content.ReadAsStringAsync();
            var addedCustomer = JsonConvert.DeserializeObject<Customer>(result);
            Assert.IsNotNull(addedCustomer);
            Assert.AreEqual("John", addedCustomer.FirstName);
        }

        //[TestMethod]
        //public async Task TestLoginService()
        //{
        //    ExternalMockService.CustomerService
        //         .Setup(x => x.GetCustomerByEmail("John@Doe.com"))
        //         .Returns(TestModels.GetTestCustomer());
        //    LoginData loginData = new LoginData { Email = "John@Doe.com", Password = "password" };
        //    var client = GetClient();
        //    var response = await client.PostAsync("/api/customer/login/", new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json"));

        //    var result = await response.Content.ReadAsStringAsync();
        //    var customer = JsonConvert.DeserializeObject<Customer>(result);
        //    Assert.IsNotNull(customer);
        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //    Assert.AreEqual("John", customer.FirstName);

        //}

    }
}
