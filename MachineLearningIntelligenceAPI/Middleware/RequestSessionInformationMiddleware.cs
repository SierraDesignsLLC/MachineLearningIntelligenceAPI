using MachineLearningIntelligenceAPI.Common;
using MachineLearningIntelligenceAPI.Common.StringConstants;
using MachineLearningIntelligenceAPI.Controllers;
using MachineLearningIntelligenceAPI.DataAccess;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using System.Text.RegularExpressions;

namespace MachineLearningIntelligenceAPI.Middleware
{
    public class RequestSessionInformationMiddleware : IMiddleware
    {
        private RequestSessionInformation RequestSessionInformation { get; set; }
        private readonly ILogger<RequestSessionInformationMiddleware> _logger;
        private IAuthService _authService { get; set; }

        /// <summary>
        /// Whitelist of endpoints that allow anonymous authentication. HashSet optimized for performance
        /// </summary>
        private static HashSet<string> _allowAnonymousWhitelist = new HashSet<string> {"/index.html", "/favicon-16x16.png", "/swagger/v1/swagger.json", "/swagger-ui-bundle.js", "/", "/swagger-ui.css", "/swagger-ui-standalone-preset.js",
            "/" + ControllerBase.BaseRoutePrefix};

        public RequestSessionInformationMiddleware(RequestSessionInformation requestSessionInformation, ILogger<RequestSessionInformationMiddleware> logger,
            IAuthService authService)
        {
            RequestSessionInformation = requestSessionInformation;
            _logger = logger;
            _authService = authService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await SetRequestSessionInformation(context.Request);
            await next(context);
        }

        internal async Task SetRequestSessionInformation(HttpRequest request)
        {
            Console.WriteLine($"RequestSessionmiddleware.cs needs to be implemented");
            return;
            /*var sessionTuple = GetIdAndTokenTupleFromHttpRequest(request);
            // if endpoint does not allow anonymous then throw 401. TODO this currently throws CORS error
            if (sessionTuple.userAccountId == null && sessionTuple.sessionToken == null && !_allowAnonymousWhitelist.Contains(request.Path))
            {
                _logger.LogError($"{request.Path.Value} does not AllowAnonymous or token is malformed: {request.Headers.Authorization}");
                throw new Exception(UnauthorizedString.UserAccountSessionUnauthorized);
            }
            if (sessionTuple.userAccountId != null)
            {
                RequestSessionInformation.RequestUserId = (Guid)sessionTuple.userAccountId;
            }
            if (sessionTuple.sessionToken != null)
            {
                RequestSessionInformation.Authentication = (Guid)sessionTuple.sessionToken;
            }
            if (RequestSessionInformation.Authentication != null)
            {
                // if session isn't valid, throw
                await _authService.AuthenticateUserAccountWithSessionToken((Guid)RequestSessionInformation.RequestUserId, (Guid)RequestSessionInformation.Authentication);
                RequestSessionInformation.HasValidatedAuthentication = true;
                var permissions = await _userAccountPermissionService.GetUserAccountPermissionsFromUserAccountId((Guid)RequestSessionInformation.RequestUserId);
                RequestSessionInformation.Permissions = permissions;
            }

            if (int.TryParse(request.Headers["X-Timezone-Offset"].ToString(), out int offset))
            {
                RequestSessionInformation.TimezoneOffset = offset;
            }
            else
            {
                RequestSessionInformation.TimezoneOffset = null;
            }

            RequestSessionInformation.Locale = request.Headers["X-Locale"].ToString() == null ? null : new CultureInfo(request.Headers["X-Locale"].ToString());*/
        }

        /// <summary>
        /// Returns ["userAccountId", "sessionToken"] from token string
        /// Needs to be protected or lower otherwise swagger breaks
        /// </summary>
        internal string[] GetTokenStrings(string token)
        {
            var tokenStrings = token.Split('.');
            if (tokenStrings.Count() != 2)
            {
                _logger.LogInformation(ErrorInformationString.UserAccountUnsuccessfulLoginAttempt + " " + token);
                throw new Exception(UnauthorizedString.UserAccountSessionUnauthorized);
            }
            return tokenStrings;
        }

        private (Guid? userAccountId, Guid? sessionToken) GetIdAndTokenTupleFromHttpRequest(HttpRequest request)
        {
            var token = GetBearerTokenFromAuthHeader(request);
            if (token == Constants.SystemUserAccountGuid.ToString())
            {
                return (Constants.SystemUserAccountGuid, null);
            }
            if (string.IsNullOrEmpty(token))
            {
                return (null, null);
                //_logger.LogInformation(ErrorInformationString.UserAccountUnsuccessfulLoginAttempt + " " + token);
                //throw new Exception(UnauthorizedString.UserAccountSessionUnauthorized);
            }

            var tokenStrings = GetTokenStrings(token);
            var userAccountId = new Guid(tokenStrings[0]);
            var sessionToken = new Guid(tokenStrings[1]);

            if (sessionToken == null)
            {
                RequestSessionInformation.HasValidatedAuthentication = false;
                _logger.LogError($"No session provided for {userAccountId}");
                throw new Exception(UnauthorizedString.UserAccountSessionUnauthorized);
            }

            return (userAccountId, sessionToken);
        }
        private string GetBearerTokenFromAuthHeader(HttpRequest request)
        {
            if (!request.Headers.ContainsKey("Authorization"))
            {
                // invalid token format
                return null;
            }
            var tokenStrings = request.Headers["Authorization"].ToString().Trim().Split("Bearer ");
            if (tokenStrings.Length != 2)
            {
                // invalid token format
                return null;
            }

            if (tokenStrings[1] == Constants.SystemUserAccountGuid.ToString())
            {
                return Constants.SystemUserAccountGuid.ToString();
            }

            var pattern = new Regex("(^.{36})\\.(.{36}$)");
            var patternNoDash = new Regex("(^.{32})\\.(.{32}$)");
            var trimmedToken = tokenStrings[1].Trim();
            if (!pattern.IsMatch(trimmedToken) && !patternNoDash.IsMatch(trimmedToken))
            {
                // invalid token format
                return null;
            }

            return trimmedToken;
        }
    }
}
