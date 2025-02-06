using MachineLearningIntelligenceAPI.DataAccess.Daos.Interfaces;
using Microsoft.Extensions.Logging;

namespace MachineLearningIntelligenceAPI.DataAccess.Daos
{
    public class AccountAutomationDataDao : IAccountAutomationDataDao
    {
        private RequestSessionInformation RequestSessionInformation { get; set; }
        private readonly ILogger<AccountAutomationDataDao> _logger;

        public AccountAutomationDataDao(RequestSessionInformation requestSessionInformation, ILogger<AccountAutomationDataDao> logger)
        {
            RequestSessionInformation = requestSessionInformation;
            _logger = logger;
        }

    }
}
