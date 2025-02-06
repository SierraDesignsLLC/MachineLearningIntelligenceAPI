namespace MachineLearningIntelligenceAPI.DTOs.Reddit
{
    public class RedditSubredditV1Dto
    {
        // Display name of the subreddit eg: r/Pizza
        public string DisplayName { get; set; }
        // Description of the subreddit eg: The home of pizza on reddit. An educational community devoted to the art of pizza making.
        public string Description { get; set; }
        // Member count of the subreddit eg 515k or 515,000
        public int MemberCount { get; set; }
        // URL of the subreddit
        public string Url { get; set; }
        // Keyword from which the subreddit matches to, if from keyword recommendation
        public string Keyword { get; set; }
        // The display name of the Account Automation in which posted to the subreddit, if for retrieving post history from account automation
        public string RecentlyPostedFrom { get; set; }
        // The account automationId, if for retrieving post history from account automation
        public string RecentlyPostedFromAutomationId { get; set; }
    }
}
