namespace MachineLearningIntelligenceAPI.DTOs.Reddit
{

    /// <summary>
    /// Reddit action results
    /// </summary>
    public class RedditActionResultsV1Dto
    {
        public List<RedditActionResultV1Dto> ActionResults { get; set; }
    }

    /// <summary>
    /// Reddit post result with http status code responses for each subreddit the post was posted to
    /// </summary>
    public class RedditActionResultV1Dto
    {
        /// <summary>
        /// Unique ID of the given action
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Pass through value to reddit service. The associated engagement job id if the reddit posts request was excecuted from a scheduled job. This could be 1:many given multiple post results using the same job.
        /// </summary>
        public string EngagementJobId { get; set; }
        /// <summary>
        /// The link (URI) to the action (if applicable). May be a comment, link to DM, link to a liked comment, etc.
        /// </summary>
        public string ContentUriLocation { get; set; }
        /// <summary>
        /// Http status code for the action as a whole when it was performed
        /// </summary>
        public HttpStatusResultV1Dto Result { get; set; }
    }
}
