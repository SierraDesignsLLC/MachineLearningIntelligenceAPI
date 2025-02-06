namespace MachineLearningIntelligenceAPI.DomainModels
{
    public class SocialMediaContent
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
        public List<Media> Media { get; set; }

        #region Equality Methods
        public override bool Equals(object obj)
        {
            var socialMediaContent = obj as SocialMediaContent;
            return socialMediaContent != null && Equals(socialMediaContent);
        }

        public bool Equals(SocialMediaContent socialMediaContent)
        {
            if (Title != socialMediaContent.Title || Message != socialMediaContent.Message
                || Media != socialMediaContent.Media)
                return false;

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ Title.GetHashCode() ^ Message.GetHashCode() ^ Media.GetHashCode();
            return hashCode;
        }
        #endregion Equality Methods
    }
}
