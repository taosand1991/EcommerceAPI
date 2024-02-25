using EcommerceAPI.Interfaces.Service;
using Moq;
using System.Reflection;

namespace Integration_Test.External_Service
{
    public class ExternalMockService
    {
        public Mock<ICustomerService> CustomerService { get; }
        public Mock<IProductService> ProductService { get; }
        public Mock<ICategoryService> CategoryService { get; }

        public ExternalMockService()
        {
            CustomerService = new Mock<ICustomerService>();
            ProductService = new Mock<IProductService>();
            CategoryService = new Mock<ICategoryService>();
        }

        public IEnumerable<(Type, Object)> GetMocks()
        {
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p =>
            {
                var underlyingType = p.PropertyType.GetGenericArguments().FirstOrDefault();
                var value = p.GetValue(this) as Mock;
                return (underlyingType, value.Object);
            })
                .ToArray();
        }
    }
}
