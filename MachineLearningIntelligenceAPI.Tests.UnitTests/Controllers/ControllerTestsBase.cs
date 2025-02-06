using MachineLearningIntelligenceAPI.DomainModels;
using MachineLearningIntelligenceAPI.Common.Auth;
using MachineLearningIntelligenceAPI.Controllers;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using Moq;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests.Controllers
{
    /// <summary>
    /// Base class for the controller tests
    /// </summary>
    public class ControllerTestsBase
    {
        public void SetupValidRequestSessionInformationWithRequestHeaderAndMocks(ControllerBase _controller, Mock<IAuthService> _mockAuthService, Guid userAccountId, Guid sessionToken)
        {
            var userPermissions = new List<UserAccountPermission> {
                new UserAccountPermission { PermissionTypeId = 3, PermissionName = FRPStrings.ContentScheduling },
                new UserAccountPermission { PermissionTypeId = 3, PermissionName = FRPStrings.Engagements }
            };
            
            _controller.RequestSessionInformation.RequestUserId = userAccountId;
            _controller.RequestSessionInformation.Authentication = sessionToken;
            _controller.RequestSessionInformation.Permissions = userPermissions;
            _controller.RequestSessionInformation.HasValidatedAuthentication = true;
            _controller.HttpContext.Request.Headers.Add("Authorization", "Bearer " + userAccountId + "." + sessionToken);
        }
    }
}
