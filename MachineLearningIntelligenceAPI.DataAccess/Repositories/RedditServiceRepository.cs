using MachineLearningIntelligenceAPI.Common;
using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MachineLearningIntelligenceAPI.Tests.UnitTests")]    // make the internal methods testable
namespace MachineLearningIntelligenceAPI.DataAccess.Repositories
{
    /// <summary>
    /// Repository class to make calls to the reddit service
    /// </summary>
    public class RedditServiceRepository : RepositoryBase, IRedditServiceRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        internal const string RedditServiceV1BaseRoutePrefix = "/api/v1/";
        internal const string RedditServiceV1DeletePost = $"post/delete";

        public RedditServiceRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient(ConnectionStrings.RedditService);
            // set up default headers, rate limit varies by endpoint
            //_httpClient.DefaultRequestHeaders.Add("RateLimit-WaitTimeMilliseconds", Constants.RedditPostWaitTimeMilliseconds.ToString());
        }

    }
}
