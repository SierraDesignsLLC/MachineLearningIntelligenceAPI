using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MachineLearningIntelligenceAPI.DataAccess.Services
{
    public class RedditService : IRedditService
    {
        private readonly ILogger<RedditService> _logger;
        private IRedditServiceRepository _redditServiceRepository { get; set; }
        public RedditService(ILogger<RedditService> logger, IRedditServiceRepository redditRepository)
        {
            _logger = logger;
            _redditServiceRepository = redditRepository;
        }

    }
}
