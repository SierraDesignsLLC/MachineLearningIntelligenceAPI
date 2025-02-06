using Moq;
using NUnit.Framework;
using MachineLearningIntelligenceAPI.Common;
using MachineLearningIntelligenceAPI.DataAccess.Repositories;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests.Repositories
{
    public class RedditServiceRepositoryTests
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private HttpClient _mockHttpClient;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;

        private RedditServiceRepository _repository;

        [SetUp]
        public void Initialize()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _mockHttpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockHttpClient.BaseAddress = new Uri("http://localhost:5000/");
            _mockHttpClientFactory.Setup(mock => mock.CreateClient(ConnectionStrings.RedditService)).Returns(_mockHttpClient);

            _repository = new RedditServiceRepository(_mockHttpClientFactory.Object);
        }

    }
}
