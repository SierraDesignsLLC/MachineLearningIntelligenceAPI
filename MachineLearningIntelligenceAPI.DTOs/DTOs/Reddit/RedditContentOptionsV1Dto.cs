namespace MachineLearningIntelligenceAPI.DTOs.Reddit
{
    public class RedditContentOptionsV1Dto
    {
        /// <summary>
        /// List of subreddits the associated content goes to
        /// </summary>
        public List<string> Subreddits { get; set; }

        /// <summary>
        /// Include whether to post to profile and hide it for future scheduling
        /// </summary>
        public bool PostToProfileForFutureScheduling { get; set; }

        /// <summary>
        /// List of poll options for a poll post. Must be 2 options min, 6 options max. 80 chars max
        /// </summary>
        public List<string> PollOptions { get; set; }

        /// <summary>
        /// Poll duration in days. Must be between 1 and 7 days
        /// </summary>
        public int PollDuration { get; set; }

        /// <summary>
        /// Link for a link post, must be valid url
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// TODO: Tags for a post. Eg. nsfw spoiler etc
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
