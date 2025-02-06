namespace MachineLearningIntelligenceAPI.Common.StringConstants
{
    /// <summary>
    /// Strings used for internal logging, not supposed to be exposed to the caller
    /// </summary>
    public static class ErrorInformationString
    {
        public const string UserAccountAlreadyExists = "UserAccount with requested username already exists";
        public const string UsernameInappropriate = "Username is not appropriate";
        public const string EmailInvalid = "Email is invalid";
        public const string PhoneInvalid = "Phone number is invalid";
        public const string PhoneCountryInvalid = "Phone number country code is invalid";
        public const string PasswordInvalid = "No password provided";
        public const string UserAccountUnsuccessfulLoginAttempt = "Unsuccessful login attempt for user account: ";
        public const string UserAccountUnsuccessfulLogoutAttempt = "Unsuccessful logout attempt for user account: ";
        public const string UserAccountNullId = "Null ID for user account username: ";
        public const string UserAccountUpdateDoesNotExist = "UserAccountId does not exist";
        public const string UserAccountNullOrEmptyPermissions = "UserAccount has null or zero permissions: ";
        public const string UserAccountNoPermission = "UserAccount does not have the requested permission: ";
    }
}
