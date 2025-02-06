using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using MachineLearningIntelligenceAPI.DataAccess.Services;
using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests.Services
{
    public class RedditServiceTests
    {
        private Mock<ILogger<RedditService>> _mockLogger;
        private Mock<IRedditServiceRepository> _mockRedditServiceRepository;

        private RedditService _service;

        [SetUp]
        public void Initialize()
        {
            _mockLogger = new Mock<ILogger<RedditService>>();
            _mockRedditServiceRepository = new Mock<IRedditServiceRepository>();

            _service = new RedditService(_mockLogger.Object, _mockRedditServiceRepository.Object);
        }

    }
}
