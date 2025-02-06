namespace MachineLearningIntelligenceAPI.DTOs
{
    public class AccountAutomationV1Dto : DtoBase<AccountAutomationV1Dto>, IDtoBase
    {
        // id of associated account automation
        public string Id { get; set; }

        public string AutomationType { get; set; }

        // status of the user. if verified, banned, active or not
        public string AutomationStatus { get; set; }

        // display name of account automation, typically your username
        public string DisplayName { get; set; }

        public string Culture { get; set; }

        #region Equality Methods

        public override bool Equals(object obj)
        {
            var accountAutomationDto = obj as AccountAutomationV1Dto;
            return accountAutomationDto != null && Equals(accountAutomationDto);
        }

        public bool Equals(AccountAutomationV1Dto accountAutomationDto)
        {
            if (!base.Equals(accountAutomationDto))
                return false;

            if (Id != accountAutomationDto.Id || AutomationType != accountAutomationDto.AutomationType || AutomationStatus != accountAutomationDto.AutomationStatus
                || DisplayName != accountAutomationDto.DisplayName || Culture != accountAutomationDto.Culture)
                return false;

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ (Id != null ? Id.GetHashCode() : 0) ^ DisplayName.GetHashCode() ^ AutomationType.GetHashCode() ^ AutomationStatus.GetHashCode()
                ^ Culture.GetHashCode();
            return hashCode;
        }

        #endregion Equality Methods
    }
}
