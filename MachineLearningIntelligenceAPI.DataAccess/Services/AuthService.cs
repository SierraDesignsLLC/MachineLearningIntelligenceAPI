using MachineLearningIntelligenceAPI.Common.StringConstants;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace MachineLearningIntelligenceAPI.DataAccess.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private IEncryptionService _encryptionService { get; set; }
        public AuthService(ILogger<AuthService> logger, IEncryptionService encryptionService)
        {
            _logger = logger;
            _encryptionService = encryptionService;
        }

        public async Task UserAccountSessionUnauthorized(string bearerToken, string informationError = ErrorInformationString.UserAccountUnsuccessfulLoginAttempt)
        {
            _logger.LogInformation(informationError + " " + bearerToken);
            throw new Exception(UnauthorizedString.UserAccountSessionUnauthorized);
        }
    }
}
