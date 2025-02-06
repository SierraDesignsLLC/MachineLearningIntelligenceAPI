namespace MachineLearningIntelligenceAPI.DomainModels
{
    public class Media
    {
        /// <summary>
        /// Byte array of media
        /// </summary>
        public byte[] Content { get; set; }
        /// <summary>
        /// Media Type. eg video, pic,
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// TODO encoding types, file formats..
        /// </summary>
        public string EncodingFormat { get; set; }
        /// <summary>
        /// File name of media file.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Caption. Used for Reddit Optional
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Link. Used for Reddit Optional
        /// </summary>
        public string Link { get; set; }

        #region Equality Methods
        public override bool Equals(object obj)
        {
            var media = obj as Media;
            return media != null && Equals(media);
        }

        public bool Equals(Media media)
        {
            if (Type != media.Type || EncodingFormat != media.EncodingFormat || FileName != media.FileName || Caption != media.Caption || Link != media.Link)
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
            hashCode = hashCode * 397 ^ Content.GetHashCode() ^ Type.GetHashCode() ^ EncodingFormat.GetHashCode() ^ FileName.GetHashCode() ^ Caption.GetHashCode() ^ Link.GetHashCode();
            return hashCode;
        }
        #endregion Equality Methods
    }
}
