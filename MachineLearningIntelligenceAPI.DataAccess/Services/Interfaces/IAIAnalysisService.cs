using MachineLearningIntelligenceAPI.DomainModels;

namespace MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces
{
    public interface IAIAnalysisService
    {
        /// <summary>
        /// Analyze the passed in user engagements from the perspective of an expert social media manager
        /// </summary>
        public Task<List<string>> AnalyzeEngagements(AnalyzeRequest analysis, string aiModel = null);
    }
}
