using MachineLearningIntelligenceAPI.Common.Enums;

namespace MachineLearningIntelligenceAPI.DomainModels
{
    public class AccountAutomationType : ModelBase<AccountAutomationType>
    {
        public AccountAutomationTypeEnum Id { get; set; } // supported ids 
        public string DisplayName { get; set; } // The system standard display name for an automation type, predominantly the website url. eg http://reddit.com
        public string AutomationDescription { get; set; } // The system standard description of the automation type explaining capabilities and any other notes


        #region Equality Methods

        public override bool Equals(object accountAutomationTypeObject)
        {
            var accountAutomationType = accountAutomationTypeObject as AccountAutomationType;
            return accountAutomationType != null && Equals(accountAutomationType);
        }

        public override bool Equals(AccountAutomationType accountAutomationType)
        {
            if (!base.Equals(accountAutomationType))
                return false;

            if (Id != accountAutomationType.Id || DisplayName != accountAutomationType.DisplayName || AutomationDescription != accountAutomationType.AutomationDescription)
                return false;

            return true;
        }

        /// <summary>
        /// Check the hash code to see if we should bother checking Equals
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = hashCode * 397 ^ Id.GetHashCode() ^ DisplayName.GetHashCode() ^ AutomationDescription.GetHashCode();
                return hashCode;
            }
        }

        #endregion Equality Methods
    }
}
