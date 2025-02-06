namespace MachineLearningIntelligenceAPI.DTOs
{
    /// <summary>
    /// Object representing a social media post.
    /// </summary>
    public class SocialMediaContentV1Dto
    {
        /// <summary>
        /// Title or caption of the content
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Body or message of the content
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Photos or video if supported, sometimes used as a singular(.Count == 1) media when platforms support a title and message for every picture or video. Then this object is a list
        /// with all the media and the individual corresponding title and messages
        /// </summary>
        public List<MediaV1Dto> Media { get; set; }
    }
}
