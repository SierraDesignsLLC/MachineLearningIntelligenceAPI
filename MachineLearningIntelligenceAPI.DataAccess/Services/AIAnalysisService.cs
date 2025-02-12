using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DomainModels;
using Microsoft.Extensions.Logging;

namespace MachineLearningIntelligenceAPI.DataAccess.Services
{
    public class AIAnalysisService : IAIAnalysisService
    {
        private readonly ILogger<AIAnalysisService> _logger;
        private IAIAnalysisRepository _aiAnalysisRepository { get; set; }
        public AIAnalysisService(ILogger<AIAnalysisService> logger, IAIAnalysisRepository aiAnalysisRepository)
        {
            _logger = logger;
            _aiAnalysisRepository = aiAnalysisRepository;
        }

        /// <summary>
        /// Analyze the passed in user engagements from the perspective of an expert social media manager
        /// </summary>
        public async Task<List<string>> AnalyzeEngagements(AnalyzeRequest analysis, string aiModel = null)
        {
            List<string> response = null;

            try
            {
                response = await _aiAnalysisRepository.AnalyzeEngagements(analysis, aiModel);
                if(response.Count != analysis.InputStrings.Count)
                {
                    // throw new Exception(); what to do here???
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }
    }
}
