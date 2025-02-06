namespace MachineLearningIntelligenceAPI.DTOs
{
    public class HttpStatusResultV1Dto
    {
        /// <summary>
        /// Http status code for the response along with the message if applicable
        /// </summary>
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
