namespace MachineLearningIntelligenceAPI.DTOs.Reddit
{

    /// <summary>
    /// Reddit post results
    /// </summary>
    public class RedditContentResultsV1Dto
    {
        public List<RedditContentResultV1Dto> PostResults { get; set; }
    }

    /// <summary>
    /// Reddit post result with http status code responses for each subreddit the post was posted to
    /// </summary>
    public class RedditContentResultV1Dto
    {
        /// <summary>
        /// Pass through value to reddit service. Unique id of the reddit post result from the original reddit content request object. 1:1
        /// </summary>
        public string ContentId { get; set; }
        /// <summary>
        /// Pass through value to reddit service. The associated account automation job id if the reddit posts request was excecuted from a scheduled job. This could be 1:many given multiple post results using the same job.
        /// </summary>
        public string AccountAutomationJobId { get; set; }
        /// <summary>
        /// The link (URI) to the post. This is used in the creation of scheduled jobs in order to store the reddit content persistently. The reddit content will be saved as a draft on the user's
        /// profile and then when the linked job is triggered.
        /// </summary>
        public string ContentUriLocation { get; set; }
        /// <summary>
        /// The results of each subreddit the post was submitted to
        /// </summary>
        public List<HttpStatusResultV1Dto> SubredditResults { get; set; }
    }
}
