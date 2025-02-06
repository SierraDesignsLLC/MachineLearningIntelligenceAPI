using MachineLearningIntelligenceAPI.Common.Enums;

namespace MachineLearningIntelligenceAPI.DomainModels.Reddit
{
    public class RedditContent : ModelBase<RedditContent>
    {
        /// <summary>
        /// Unique id of the linked account automation
        /// </summary>
        public string AccountAutomationId { get; set; }
        /// <summary>
        /// The associated account automation job id if the reddit posts request was excecuted from a scheduled job
        /// </summary>
        public string AccountAutomationJobId { get; set; }
        /// <summary>
        /// The link (URI) to the post. This is used in the creation of scheduled jobs in order to store the reddit content persistently. The reddit content will be saved as a draft on the user's
        /// profile and then when the linked job is triggered, if this uri is populated, it will retrieve the data from the uri to post to reddit
        /// </summary>
        public string ContentUriLocation { get; set; }

        /// <summary>
        /// The social media content that makes up a reddit post
        /// </summary>
        public SocialMediaContent Content { get; set; }

        /// <summary>
        /// Reddit content options
        /// </summary>
        public RedditContentOptions ContentOptions { get; set; }

        /// <summary>
        /// Post type <see cref="RedditPostType"/>
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Client Id for OAuth
        /// </summary>
        public string OAuthClientId { get; set; }

        /// <summary>
        /// Refresh token for OAuth
        /// </summary>
        public string OAuthRefreshToken { get; set; }

        #region Equality Methods

        public override bool Equals(object obj)
        {
            var redditContent = obj as RedditContent;
            return redditContent != null && Equals(redditContent);
        }

        public override bool Equals(RedditContent redditContent)
        {
            if (!base.Equals(redditContent))
                return false;

            if (AccountAutomationId != redditContent.AccountAutomationId || Content != redditContent.Content
                || ContentOptions != redditContent.ContentOptions || Type != redditContent.Type || OAuthClientId != redditContent.OAuthClientId || OAuthRefreshToken != OAuthRefreshToken)
                return false;

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ AccountAutomationId.GetHashCode() ^ Content.GetHashCode() ^ ContentOptions.GetHashCode() ^ Type.GetHashCode()
                ^ OAuthClientId.GetHashCode() ^ OAuthRefreshToken.GetHashCode();
            return hashCode;
        }

        #endregion Equality Methods
    }
}
