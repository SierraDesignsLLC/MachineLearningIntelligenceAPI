namespace MachineLearningIntelligenceAPI.DTOs
{
    /// <summary>
    /// Generic Dto class that contains a list of succeeded and failed ids 
    /// </summary>
    public class EntityIdResultsV1Dto
    {
        public List<string> SucceededIds { get; set; }
        public List<string> FailedIds { get; set; }
    }
}
