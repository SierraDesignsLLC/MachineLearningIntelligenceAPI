using System.Net;
/// <summary>
/// This file contains matching classes for given http response codes and their corresponding error messages that exist for them. Safe to use in any layer as they are not coupled to anything specific
/// NEVER RETURN PII OR PPI IN ANY MESSAGES HERE OR GIVE INFORMATION INTO HOW THE SERVICE WORKS
/// </summary>
namespace MachineLearningIntelligenceAPI.Common.StringConstants
{
    /// <summary>
    /// The class that contains error messages about 400 bad requests
    /// </summary>
    public static class BadRequestString
    {
        public const string MissingFields = "Request must contain all required fields";
        public const string UserAccountUnavailable = "Username not available";
        public const string UserAccountInvalid = "User Account is invalid";
        public const string EmailInvalid = "Email is invalid";
        public const string PhoneInvalid = "Phone number is invalid";
        public const string PhoneCountryInvalid = "Phone number country code is invalid";
        public const string PasswordInvalid = "No password provided";
        public const string MaximumUsernameLength = "Username exceeds maximum length of 50 characters";
        public const string MaximumEmailLength = "Email exceeds maximum length of 100 characters";
        public const string RequiredPhoneLength = "Phone doesn't equal required length of 10 characters";
        public const string RepositoryError = "Dependent service returned the bad request";
        public const string RedditMixedMediaTypesUnsupported = "Reddit does not support posting video and picture files to the same post";
        public const string RedditMultipleVideosUnsupported = "Reddit does not support posting multiple videos to the same post";
        public const string RedditLinkInvalid = "Reddit requires a valid URL for a link post";
        public const string RedditPostsTypeInvalid = "Reddit post type invalid";
        public const string RedditBadRequest = "Reddit failed to yield any subreddit results, please verify your posting parameters";
        public const string AccountAutomationInvalid = "Account automation is invalid";
        public const string AccountAutomationJobPayloadInvalid = "This automation's job has content that is invalid, please check the format of the content";
        public const string AccountAutomationJobStatusInvalid = "This automation's job cannot be scheduled, please try again later; or relink the account, check that the job is valid, and that the 3rd party website is available";
        public const string EngagementJobInvalid = "Engagement job failed, this automation's job is invalid";
        public const string EngagementRuleInvalid = "Engagement rule is invalid";
        public const string EngagementRuleLimitExceeded = "Engagement rule limit exceeded";
        public const string MaximumEngagementNameLength = "Name exceeds maximum length of 50 characters";
        public const string MaximumEngagementDescriptionLength = "Description exceeds maximum length of 75 characters";

    }
    /// <summary>
    /// The class that contains error messages about 401 unauthorized
    /// </summary>
    public static class UnauthorizedString
    {
        public const string UserAccountUnauthorized = "Username or password is incorrect";
        public const string UserAccountSessionUnauthorized = "Session is invalid or expired";
        public const string OriginalPasswordUnauthorized = "Original password is incorrect";
        public const string RoleUnauthorized = "Role is incorrect";
        public const string RepositoryError = "Dependent service has incorrect credentials";
        public const string AccountAutomationUnauthorized = "This automation is unauthorized, check your credentials";
    }
    /// <summary>
    /// The class that contains error messages about 403 forbidden
    /// </summary>
    public static class ForbiddenString
    {
        public const string UserAccountUpdateForbidden = "Elevated credentials required to perform this update";
        public const string InfrastructureForbidden = "Elevated credentials required to utilize this infrastructure";
        public const string FeatureForbidden = "Elevated credentials required to utilize this feature";
        public const string RepositoryError = "Elevated credentials required to utilize this dependent service";
        public const string AccountAutomationForbidden = "This automation has been reported to be banned from performing this action";
    }
    /// <summary>
    /// The class that contains error messages about 404 not found
    /// </summary>
    public static class NotFoundString
    {
    }
    /// <summary>
    /// The class that contains error messages about 409 conflict
    /// </summary>
    public static class ConflictString
    {
        public const string AccountAutomationAlreadyExists = "Account automation already exists";
    }
    /// <summary>
    /// The class that contains error messages about 429 too many requests
    /// </summary>
    public static class TooManyRequestsString
    {
        public const string RedditTooManyRequests = "Exceeded the number of requests that Reddit allows, you will need to wait a moment before making another request";
    }
    /// <summary>
    /// The class that contains error messages about 500 internal server error
    /// </summary>
    public static class InternalServerErrorString
    {
        public const string RepositoryError = "Internal server error from dependent service";
        public const string AccountAutomationError = "This automation has an error, try readding it";
        public const string MissingRequestUserIdError = "Incoming request user id information is missing and must be present either with a system id or user id";
        public const string EngagementJobRulesError = "Engagement job's rule is invalid, no actions found";
        public const string OpenApiKeyNullError = "OpenApi key is null! Throwing";
    }
    /// <summary>
    /// The class that contains error messages about 503 service unavailable
    /// </summary>
    public static class ServiceUnavailableString
    {
        public const string RepositoryError = "Dependent service is unavailable at this time";
    }

    /// <summary>
    /// Dictionary lookup
    /// </summary>
    public static class ErrorResponses
    {
        // TODO: dynamically create this dictionary, is okay to use reflection?
        public static Dictionary<string, int> Dictionary = new Dictionary<string, int>()
        {
            // 400
            { BadRequestString.MissingFields, (int)HttpStatusCode.BadRequest},
            { BadRequestString.UserAccountUnavailable, (int)HttpStatusCode.BadRequest},
            { BadRequestString.UserAccountInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.EmailInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.PhoneInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.PhoneCountryInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.PasswordInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.MaximumUsernameLength, (int)HttpStatusCode.BadRequest},
            { BadRequestString.MaximumEmailLength, (int)HttpStatusCode.BadRequest},
            { BadRequestString.RequiredPhoneLength, (int)HttpStatusCode.BadRequest},
            { BadRequestString.RepositoryError, (int)HttpStatusCode.BadRequest},
            { BadRequestString.RedditMixedMediaTypesUnsupported, (int)HttpStatusCode.BadRequest},
            { BadRequestString.RedditMultipleVideosUnsupported, (int)HttpStatusCode.BadRequest},
            { BadRequestString.RedditLinkInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.RedditPostsTypeInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.RedditBadRequest, (int)HttpStatusCode.BadRequest},
            { BadRequestString.AccountAutomationInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.AccountAutomationJobPayloadInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.AccountAutomationJobStatusInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.EngagementJobInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.EngagementRuleInvalid, (int)HttpStatusCode.BadRequest},
            { BadRequestString.EngagementRuleLimitExceeded, (int)HttpStatusCode.BadRequest},
            { BadRequestString.MaximumEngagementNameLength, (int)HttpStatusCode.BadRequest},
            { BadRequestString.MaximumEngagementDescriptionLength, (int)HttpStatusCode.BadRequest},
            // 401
            { UnauthorizedString.UserAccountUnauthorized, (int)HttpStatusCode.Unauthorized},
            { UnauthorizedString.UserAccountSessionUnauthorized, (int)HttpStatusCode.Unauthorized},
            { UnauthorizedString.OriginalPasswordUnauthorized, (int)HttpStatusCode.Unauthorized},
            { UnauthorizedString.RoleUnauthorized, (int)HttpStatusCode.Unauthorized},
            { UnauthorizedString.RepositoryError, (int)HttpStatusCode.Unauthorized},
            { UnauthorizedString.AccountAutomationUnauthorized, (int)HttpStatusCode.Unauthorized},
            // 403
            { ForbiddenString.UserAccountUpdateForbidden, (int)HttpStatusCode.Forbidden},
            { ForbiddenString.InfrastructureForbidden, (int)HttpStatusCode.Forbidden},
            { ForbiddenString.FeatureForbidden, (int)HttpStatusCode.Forbidden},
            { ForbiddenString.RepositoryError, (int)HttpStatusCode.Forbidden},
            { ForbiddenString.AccountAutomationForbidden, (int)HttpStatusCode.Forbidden},
            // 404

            // 409
            { ConflictString.AccountAutomationAlreadyExists, (int)HttpStatusCode.Conflict},
            // 429
            { TooManyRequestsString.RedditTooManyRequests, (int)HttpStatusCode.TooManyRequests},
            // 500
            { InternalServerErrorString.RepositoryError, (int)HttpStatusCode.InternalServerError},
            { InternalServerErrorString.AccountAutomationError, (int)HttpStatusCode.InternalServerError},
            { InternalServerErrorString.MissingRequestUserIdError, (int)HttpStatusCode.InternalServerError},
            // 503
            { ServiceUnavailableString.RepositoryError, (int)HttpStatusCode.ServiceUnavailable},
        };
    }
}
