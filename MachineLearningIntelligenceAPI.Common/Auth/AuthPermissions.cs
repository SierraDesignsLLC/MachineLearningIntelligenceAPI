namespace MachineLearningIntelligenceAPI.Common.Auth
{
    /// <summary>
    /// Policy-based authorization gives you more flexibility. You can use custom authorization handlers with policies to add more complex logic than 
    /// just checking if your user has a specific role. For example, you have some roles mappings in your database. You can create a policy that will 
    /// check if your user is authorized according to that data or that can be any custom logic.
    /// 
    /// On user creation, users will be granted roles. The roles will correspond to which IACs and FRPs they have. 
    /// 
    /// Example policies: activesession, requires an active session for the given caller. Policies are the evalutation of what the conditions are for roles iacs and frps
    /// Example roles: admin, base user, premium user, etc... Roles can grant IACs and FRPs. 
    /// Example IACs: RedditService, the infrastructure access control to allow the caller the reddit integration to reach out to another service and enables the reddit integration functionality
    /// Example FRPs: RedditPostScheduler, the feature resource permission to allow the granular permission of scheduling posts to be created on reddit
    /// 
    /// In the database there is 1 Table for user_account_permissions. It has a type: iac or frp, which is linked to user_accountid. with the string value for the name
    /// </summary>
    // TODO: this file should probably just store Policies and IACs because it is controller level. Roles potentially? FRPs probably not, because those are more granular


    public class AuthPolicyStrings
    {
        public const string ActiveSession = nameof(ActiveSession);
    }
    public class AuthRoleStrings
    {
        /// <summary>
        /// Default user level upon registration
        /// </summary>
        public const string Tier1User = nameof(Tier1User);
        public const string Tier2User = nameof(Tier2User);
        public const string Tier3User = nameof(Tier3User);
    }

    /// <summary>
    /// Infrastructure Access Control strings. WARNING, if these are ever renamed, the database must be updated in conjunction
    /// </summary>
    public class IACStrings
    {
        /// <summary>
        /// Allows oauth integration with reddit and access to the reddit microservice
        /// </summary>
        public const string RedditService = nameof(RedditService);

        /// <summary>
        /// Allows the nightly retrieval of account automation data for users' automations. Stats and metrics for the automation
        /// </summary>
        public const string AccountAutomationData = nameof(AccountAutomationData);
    }

    /// <summary>
    /// Feature Resource Permission strings. WARNING, if these are ever renamed, the database must be updated in conjunction
    /// </summary>
    public class FRPStrings
    {
        /// <summary>
        /// Allows accessing the posting feature for reddit
        /// </summary>
        public const string RedditPosting = nameof(RedditPosting);
        /// <summary>
        /// Allows accessing the the advanced searches/analytics for reddit
        /// </summary>
        public const string RedditAnalytics = nameof(RedditAnalytics);
        /// <summary>
        /// Allows scheduling content using account automation jobs
        /// </summary>
        public const string ContentScheduling = nameof(ContentScheduling);
        /// <summary>
        /// Allows using engagement rules to configure engagement on an account automation's platform
        /// </summary>
        public const string Engagements = nameof(Engagements);
        public const string EngagementsTier2 = nameof(EngagementsTier2);    // tier 2
        public const string EngagementsTier3 = nameof(EngagementsTier3);    // tier 3

    }

    /// <summary>
    /// Dictionary lookup for which IACs, FRPs are included in a role
    /// </summary>
    public static class RolePermissions
    {
        // TODO: dynamically create this dictionary?
        // dictionary key is role. value is tuple, first list of string are the iacs and the second list of string are the frps
        // dictionaries are additive. Tier 2 contains all tier 1, tier 3 contains all tier 2 and tier 1
        public static Dictionary<string, (List<string> IACs, List<string> FRPs)> Dictionary = new Dictionary<string, (List<string> IACs, List<string> FRPs)>()
        {
            {
                AuthRoleStrings.Tier1User,
                (IACs: new List<string>{ IACStrings.RedditService },
                FRPs: new List<string>{ FRPStrings.RedditPosting, FRPStrings.RedditAnalytics, FRPStrings.ContentScheduling, FRPStrings.Engagements })
            },
            {
                AuthRoleStrings.Tier2User,
                (IACs: new List<string>{ IACStrings.AccountAutomationData },
                FRPs: new List<string>{ FRPStrings.EngagementsTier2 })
            },
            {
                AuthRoleStrings.Tier3User,
                (IACs: new List<string>{},
                FRPs: new List<string>{ FRPStrings.EngagementsTier3 })
            },
        };

    }

}
