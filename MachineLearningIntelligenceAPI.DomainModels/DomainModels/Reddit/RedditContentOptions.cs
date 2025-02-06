namespace MachineLearningIntelligenceAPI.DomainModels.Reddit
{
    public class RedditContentOptions
    {
        /// <summary>
        /// List of subreddits the associated content goes to
        /// </summary>
        public List<string> Subreddits { get; set; }

        /// <summary>
        /// Include whether to post to profile
        /// </summary>
        public bool HidePostForFutureScheduling { get; set; } = false;

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

        #region Equality Methods
        public override bool Equals(object obj)
        {
            var options = obj as RedditContentOptions;
            return options != null && Equals(options);
        }

        public bool Equals(RedditContentOptions options)
        {
            if (Subreddits != options.Subreddits || PollOptions != options.PollOptions || Link != options.Link || Tags != options.Tags)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ Subreddits.GetHashCode() ^ PollOptions.GetHashCode() ^ Link.GetHashCode() ^ Tags.GetHashCode();
            return hashCode;
        }
        #endregion Equality Methods
    }
}
