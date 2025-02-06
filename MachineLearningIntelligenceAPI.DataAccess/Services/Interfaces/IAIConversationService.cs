
using MachineLearningIntelligenceAPI.DomainModels;

namespace MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces
{
    public interface IAIConversationService
    {
        /// <summary>
        /// Talk to AI with given input text. Get response from AI as string
        /// </summary>
        public Task<string> TalkToAI(ConversationRequest conversation, string aiModel = null);
    }
}
