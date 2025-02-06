using MachineLearningIntelligenceAPI.Common;
using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MachineLearningIntelligenceAPI.DataAccess.Repositories
{
    public class MachineLearningIntelligenceAPIRepository : IMachineLearningIntelligenceAPIRepository
    {
        private readonly ILogger<MachineLearningIntelligenceAPIRepository> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        public MachineLearningIntelligenceAPIRepository(ILogger<MachineLearningIntelligenceAPIRepository> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient(ConnectionStrings.MachineLearningIntelligenceAPI);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{Constants.SystemUserAccountGuid}");   // set the system guid to know the caller is coming from the system. TODO security, do not implement sensitive endpoints until there is security
        }

        /// <summary>
        /// Gets and runs scheduled jobs, designed for distributed computing where multiple servers should avoid accessing the same entities. Need to break out scheduler service to its own service
        /// Run from the background service to an http call to properly setup dependency injection and not run from the background service
        /// </summary>
        public void GetAndRunScheduledAccountAutomationJobs(bool retry = false)
        {
            // fire and forget http call
            if (retry)
            {
                _httpClient.PostAsync("v1/accountAutomationJob/run?retryFailedJobs=true", null);
            }
            if (!retry)
            {
                _httpClient.PostAsync("v1/accountAutomationJob/run", null);
            }

        }

        /// <summary>
        /// Gets and runs scheduled jobs, designed for distributed computing where multiple servers should avoid accessing the same entities. Need to break out scheduler service to its own service
        /// Run from the background service to an http call to properly setup dependency injection and not run from the background service
        /// </summary>
        public void GetAndRunCommunityEngagementJobs()
        {
            // fire and forget http call
            _httpClient.PostAsync("v1/engagementJob/run", null);

        }

        /// <summary>
        /// Create account automation data for provided IDs
        /// </summary>
        public void CreateAccountAutomationData(Guid userAccountId, List<Guid?> accountAutomationIdsToRun)
        {
            var json = JsonSerializer.Serialize(accountAutomationIdsToRun);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // fire and forget http call
            _httpClient.PostAsync($"v1/accountAutomationData/run/{userAccountId}", content);
        }

        /// <summary>
        /// Run create account automation data for premium users. This is called from a nightly batch job 
        /// </summary>
        public void RunAccountAutomationDataJob()
        {
            // fire and forget http call
            _httpClient.PostAsync($"v1/accountAutomationData/run/all", null);
        }
    }
}
