using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DomainModels;
using Microsoft.Extensions.Logging;

namespace MachineLearningIntelligenceAPI.DataAccess.Services
{
    public class AIConversationService : IAIConversationService
    {
        private readonly ILogger<AIConversationService> _logger;
        private IAIConversationRepository _aiConversationRepository { get; set; }
        public AIConversationService(ILogger<AIConversationService> logger, IAIConversationRepository aiConversationRepository)
        {
            _logger = logger;
            _aiConversationRepository = aiConversationRepository;
        }

        /// <summary>
        /// Talk to AI with given input text. Get response from AI as string
        /// </summary>
        public async Task<string> TalkToAI(ConversationRequest conversation, string aiModel = null)
        {
            string response = null;

            try
            {
                response = await _aiConversationRepository.TalkToAI(conversation, aiModel);
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }
    }
}
