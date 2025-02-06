namespace MachineLearningIntelligenceAPI.DataAccess.Daos
{
    /// <summary>
    /// String constants of the database schema strings, used for mapping
    /// </summary>
    public class DaoEntityBaseConstants
    {
        /// <summary>
        /// Common shared columns that should be standardized
        /// </summary>
        public const string Created = "created";
        public const string CreatedBy = "created_by";
        public const string Modified = "modified";
        public const string ModifiedBy = "modified_by";
    }

    /// <summary>
    /// User_account
    /// </summary>
    public class DaoUserAccountStrings
    {
        public const string Table = "user_account";
        public const string Id = "user_account_id";
        public const string Username = "username";
        public const string PasswordEncrypted = "password_encrypted";
        public const string Salt = "salt";
        public const string Email = "email";
        public const string PhoneNumber = "phone_number";
        public const string PhoneNumberCountryCode = "phone_number_country_code";
        public const string Culture = "culture";
    }

    /// <summary>
    /// User_account_session
    /// </summary>
    public class DaoUserAccountSessionStrings
    {
        public const string Table = "user_account_session";
        public const string Id = "user_account_session_id";
        public const string SessionToken = "session_token";
        public const string UserAccountId = "user_account_id";
        public const string Expires = "expires";
    }

    /// <summary>
    /// User_account_permission
    /// </summary>
    public class DaoUserAccountPermissionStrings
    {
        public const string Table = "user_account_permission";
        public const string Id = "user_account_permission_id";
        public const string UserAccountId = "user_account_id";
        public const string PermissionName = "permission_name";
        public const string PermissionType = "permission_type";
    }

    /// <summary>
    /// User_account_permission_type
    /// </summary>
    public class DaoUserAccountPermissionTypeStrings
    {
        public const string Table = "user_account_permission_type";
        public const string Id = "user_account_permission_type_id";
        public const string PermissionType = "permission_type";
        public const string PermissionDescription = "permission_description";
    }

    /// <summary>
    /// Account_automation
    /// </summary>
    public class DaoAccountAutomationStrings
    {
        public const string Table = "account_automation";
        public const string Id = "account_automation_id";
        public const string UserAccountId = "user_account_id";
        public const string UsernameOrEmailEncrypted = "username_or_email_encrypted";
        public const string UsernameOrEmailPrivateKey = "username_or_email_private_key";
        public const string UsernameOrEmailInitializationVector = "username_or_email_initialization_vector";
        public const string PasswordEncrypted = "password_encrypted";
        public const string PasswordPrivateKey = "password_private_key";
        public const string PasswordInitializationVector = "password_initialization_vector";
        public const string ClientId = "client_id";
        public const string AccessToken = "access_token";
        public const string RefreshToken = "refresh_token";
        public const string AutomationType = "automation_type";
        public const string AutomationStatus = "automation_status";
        public const string DisplayName = "display_name";
        public const string Culture = "culture";
    }

    /// <summary>
    /// Account_automation_data
    /// </summary>
    public class DaoAccountAutomationDataStrings
    {
        public const string Table = "account_automation_data";
        public const string Id = "account_automation_data_id";
        public const string AccountAutomationId = "account_automation_id";
        public const string PostCount = "post_count";
        public const string ClicksCount = "clicks_count";
        public const string ViewsCount = "views_count";
        public const string LikesCount = "likes_count";
        public const string DirectMessageCount = "direct_message_count";
        public const string FollowersCount = "followers_count";
        public const string SubscribersCount = "subscribers_count";
        public const string CommentsCount = "comments_count";
        public const string SharesCount = "shares_count";
        public const string SavesCount = "saves_count";
    }

    /// <summary>
    /// Account_automation_job
    /// </summary>
    public class DaoAccountAutomationJobStrings
    {
        public const string Table = "account_automation_job";
        public const string Id = "account_automation_job_id";
        public const string AccountAutomationId = "account_automation_id";
        public const string AccountAutomationJobStatus = "account_automation_job_status";
        public const string TriggerOnUTC = "trigger_on_utc";
        public const string Payload = "payload";
    }

    /// <summary>
    /// engagement_rule
    /// </summary>
    public class DaoEngagementRuleStrings
    {
        public const string Table = "engagement_rule";
        public const string Id = "engagement_rule_id";
        public const string UserAccountId = "user_account_id";
        public const string RuleName = "rule_name";
        public const string RuleDescription = "rule_description";
        public const string RuleParameterSets = "rule_parameter_sets";
    }

    /// <summary>
    /// engagement_job
    /// </summary>
    public class DaoEngagementJobStrings
    {
        public const string Table = "engagement_job";
        public const string Id = "engagement_job_id";
        public const string AccountAutomationId = "account_automation_id";
        public const string EngagementJobMetadata = "engagement_job_metadata";
        public const string Status = "engagement_job_status";
        public const string TriggerOnUTC = "trigger_on_utc";
        public const string Interval = "trigger_interval";
        public const string ExecutionCount = "execution_count";
    }

    /// <summary>
    /// Account_automation_type
    /// </summary>
    public class DaoAccountAutomationTypeStrings
    {
        public const string Table = "account_automation_type";
        public const string Id = "account_automation_type_id";
        public const string DisplayName = "display_name";
        public const string AutomationDescription = "automation_description";
    }

}
