using MachineLearningIntelligenceAPI.Common.Enums;

namespace MachineLearningIntelligenceAPI.DTOs.Reddit
{
    /// <summary>
    /// This contains auth data for a reddit account automation and should only be used for internal services
    /// </summary>
    public class RedditAccountAutomationV1Dto : AccountAutomationV1Dto
    {
        // refresh token returned from reddit
        public string RefreshToken { get; set; }
        // clientId from reddit, specifically for oauth
        public string ClientId { get; set; }

        // name from reddit, used to set the display name
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; DisplayName = value; }
        }

        // if the user if verified on reddit or not. if not the user won't have access to features
        private bool _verified;
        public bool Verified
        {
            get { return _verified; }
            set { _verified = value; AutomationStatus = value ? AccountAutomationStatusEnum.Active.ToString() : AccountAutomationStatusEnum.Unverified.ToString(); }
        }
    }
}
