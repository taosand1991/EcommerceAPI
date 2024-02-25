namespace Integration_Test.External_Service
{
    public class CustomBaseTest
    {
        private readonly CustomWebApplicatonFactory _webApplicatonFactory;
        public ExternalMockService ExternalMockService { get; }

        public CustomBaseTest()
        {
            ExternalMockService = new ExternalMockService();
            _webApplicatonFactory = new CustomWebApplicatonFactory(ExternalMockService);
        }

        public HttpClient GetClient() => _webApplicatonFactory.CreateClient();

    }
}
