namespace MachineLearningIntelligenceAPI.DomainModels
{
    public class AccountAutomation : ModelBase<AccountAutomation>
    {
        public string UserAccountId { get; set; }
        public string? UsernameOrEmailEncrypted { get; set; } // username or email from the account's platform that authorized with client credentials
        public string? UsernameOrEmailPrivateKey { get; set; } // client credentials
        public string? UsernameOrEmailInitializationVector { get; set; } // client credentials
        public string? PasswordEncrypted { get; set; } // client credentials
        public string? PasswordPrivateKey { get; set; } // client credentials
        public string? PasswordInitializationVector { get; set; } // client credentials
        public string? ClientId { get; set; } // client id from the account's platform that authorized the oauth connection
        public string? AccessToken { get; set; } // oauth
        public string? RefreshToken { get; set; } // oauth
        public int AutomationType { get; set; }
        public int AutomationStatus { get; set; }
        public string DisplayName { get; set; } // display name for the given account automation
        public string? Culture { get; set; }

        #region Equality Methods

        public override bool Equals(object obj)
        {
            var accountAutomation = obj as AccountAutomation;
            return accountAutomation != null && Equals(accountAutomation);
        }

        public override bool Equals(AccountAutomation accountAutomation)
        {
            if (!base.Equals(accountAutomation))
                return false;

            if (UserAccountId != accountAutomation.UserAccountId || UsernameOrEmailEncrypted != accountAutomation.UsernameOrEmailEncrypted
                || PasswordEncrypted != accountAutomation.PasswordEncrypted)
                return false;

            if (AutomationType != accountAutomation.AutomationType || AutomationStatus != accountAutomation.AutomationStatus || DisplayName != accountAutomation.DisplayName || Culture != accountAutomation.Culture)
                return false;

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ UserAccountId.GetHashCode() ^ (UsernameOrEmailEncrypted != null ? UsernameOrEmailEncrypted.GetHashCode() : 0)
            ^ (PasswordEncrypted != null ? PasswordEncrypted.GetHashCode() : 0) ^ AutomationType.GetHashCode() ^ AutomationType.GetHashCode()
            ^ (DisplayName != null ? AutomationStatus.GetHashCode() : 0) ^ (Culture != null ? Culture.GetHashCode() : 0);
            return hashCode;
        }

        #endregion Equality Methods
    }
}
