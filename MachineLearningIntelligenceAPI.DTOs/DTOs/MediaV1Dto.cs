namespace MachineLearningIntelligenceAPI.DTOs
{
    public class MediaV1Dto
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
    }
}
