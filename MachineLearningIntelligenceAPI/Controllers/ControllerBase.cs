using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;
using MachineLearningIntelligenceAPI.Common.StringConstants;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess;
using MachineLearningIntelligenceAPI.DomainModels;

[assembly: InternalsVisibleTo("MachineLearningIntelligenceAPI.Tests.UnitTests")]    // make the internal methods testable
namespace MachineLearningIntelligenceAPI.Controllers
{
    /// <summary>
    /// Base class for all controller endpoints
    /// </summary>
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        public const string BaseRoutePrefix = "api/";
        public RequestSessionInformation RequestSessionInformation { get; set; }
        // handle http response errors. common functionality across all controllers
        // handle auth stuff here, auth attributes? Cors

        private readonly ILogger<ControllerBase> _logger;
        private IAuthService _authService { get; set; }

        public ControllerBase(RequestSessionInformation requestSessionInformation, ILogger<ControllerBase> logger, IAuthService authService)
        {
            RequestSessionInformation = requestSessionInformation;
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// Handles policy authorization for a caller
        /// Needs to be protected or lower otherwise swagger breaks
        /// </summary>
        internal async Task AuthorizeWithPolicy(string policyName)
        {
            
        }

        /// <summary>
        /// Handles Infrastructure Access Control authorization for a caller and
        /// handles Feature Resource Permission authorization for a caller with passed in required grantedPermissions and passed in user grantedPermissions
        /// Needs to be protected or lower otherwise swagger breaks
        /// </summary>
        internal async Task AuthorizePermissions(List<(string PermissionType, string PermissionName)> requiredPermissions)
        {
            var grantedPermissions = RequestSessionInformation.Permissions ?? new List<UserAccountPermission>();
            if (grantedPermissions == null)
            {
                // user has no iacs or frps
                _logger.LogInformation(ErrorInformationString.UserAccountNullOrEmptyPermissions + RequestSessionInformation.RequestUserId);
                throw new Exception(ForbiddenString.InfrastructureForbidden);
            }
        }

        /// <summary>
        /// Handles exceptions from business layer and then gives http response with sanitized message.
        /// Needs to be protected or lower otherwise swagger breaks
        /// </summary>
        internal ObjectResult GenerateHttpResponseFromException(Exception ex)
        {
            if (ErrorResponses.Dictionary.ContainsKey(ex.Message))
            {
                var errorResponseLookup = ErrorResponses.Dictionary[ex.Message];

                if (errorResponseLookup == (int)HttpStatusCode.BadRequest)
                {
                    return BadRequest(ex.Message);
                }

                if (errorResponseLookup == (int)HttpStatusCode.Unauthorized)
                {
                    return Unauthorized(ex.Message);
                }

                if (errorResponseLookup == (int)HttpStatusCode.Forbidden)
                {
                    var forbiddenObjectResult = new ObjectResult(ex.Message);
                    forbiddenObjectResult.StatusCode = (int)HttpStatusCode.Forbidden;
                    return forbiddenObjectResult;
                }

                if (errorResponseLookup == (int)HttpStatusCode.Conflict)
                {
                    return Conflict(ex.Message);
                }

                if (errorResponseLookup == (int)HttpStatusCode.TooManyRequests)
                {
                    var tooManyRequestsObjectResult = new ObjectResult(ex.Message);
                    tooManyRequestsObjectResult.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    return tooManyRequestsObjectResult;
                }
            }

            return Problem("500");  // this is the default 500 error. 
        }

    }
}
