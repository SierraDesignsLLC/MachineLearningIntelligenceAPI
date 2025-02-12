using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DomainModels;
using Microsoft.Extensions.Logging;

namespace MachineLearningIntelligenceAPI.DataAccess.Services
{
    public class AITranslationService : IAITranslationService
    {
        private readonly ILogger<AITranslationService> _logger;
        private IAITranslationRepository _aiTranslationRepository { get; set; }
        public AITranslationService(ILogger<AITranslationService> logger, IAITranslationRepository aiTranslationRepository)
        {
            _logger = logger;
            _aiTranslationRepository = aiTranslationRepository;
        }

        /// <summary>
        /// Translation with AI with given input text, and language. Get response from AI as string
        /// </summary>
        public async Task<string> TranslationWithAI(TranslationRequest conversation, string aiModel = null)
        {
            string response = null;

            try
            {
                response = await _aiTranslationRepository.TranslationWithAI(conversation, aiModel);
                // serialize the response into string array .length is string length
                /*if(response.Length != conversation.InputStrings.Count)
                {
                    // throw new Exception(); what to do here???
                }*/
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }
    }
}
