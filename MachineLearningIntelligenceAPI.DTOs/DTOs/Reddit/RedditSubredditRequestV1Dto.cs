namespace MachineLearningIntelligenceAPI.DTOs.Reddit
{
    public class RedditSubredditRequestV1Dto
    {
        // Keyword to search for subreddits with
        public string Keyword { get; set; }
        // Number of subreddits to return
        public int SubredditsToReturn { get; set; } = 10;
        // Client Id for OAuth
        public string OAuthClientId { get; set; }
        // Refresh token for OAuth
        public string OAuthRefreshToken { get; set; }
    }
}
