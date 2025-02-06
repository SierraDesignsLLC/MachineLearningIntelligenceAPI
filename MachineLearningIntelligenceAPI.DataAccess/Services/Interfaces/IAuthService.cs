using MachineLearningIntelligenceAPI.Common.StringConstants;

namespace MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces
{
    public interface IAuthService
    {
        public Task UserAccountSessionUnauthorized(string bearerToken, string informationError = ErrorInformationString.UserAccountUnsuccessfulLoginAttempt);
    }
}
