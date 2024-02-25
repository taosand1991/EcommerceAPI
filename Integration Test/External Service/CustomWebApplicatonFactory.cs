using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;


namespace Integration_Test.External_Service
{
    public class CustomWebApplicatonFactory : WebApplicationFactory<Program>
    {
        private readonly ExternalMockService _externalMockService;

        public CustomWebApplicatonFactory(ExternalMockService externalMockService)
        {
            _externalMockService = externalMockService;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                foreach ((var interfaceType, var serviceMock) in _externalMockService.GetMocks())
                {
                    services.Remove(services.SingleOrDefault(d => d.ServiceType == interfaceType));
                    services.AddSingleton(interfaceType, serviceMock);
                }
            });
        }
    }
}
