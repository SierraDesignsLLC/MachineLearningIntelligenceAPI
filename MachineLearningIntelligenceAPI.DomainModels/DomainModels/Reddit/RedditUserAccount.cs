using MachineLearningIntelligenceAPI.Common.Enums;

namespace MachineLearningIntelligenceAPI.DomainModels.Reddit
{
    public class RedditUserAccount
    {
        // AccountAutomationId associated with the account automation, can be null
        public string AccountAutomationId { get; set; }
        // refresh token returned from reddit
        public string RefreshToken { get; set; }
        // clientId from reddit, specifically for oauth, this is the ID of the actual app integration itself, not the account
        public string ClientId { get; set; }
        // display name on reddit, typically your reddit username
        public string Name { get; set; }
        // if the user if verified on reddit or not. if not the user won't have access to features
        public AccountAutomationStatusEnum Status { get; set; }
    }
}
