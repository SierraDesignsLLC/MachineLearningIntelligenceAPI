using MachineLearningIntelligenceAPI.DomainModels;

namespace MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces
{
    public interface IAITranslationRepository
    {
        /// <summary>
        /// Translate with AI with given input strings. Get response from AI as strings 1: 1
        /// </summary>
        public Task<string> TranslationWithAI(TranslationRequest conversation, string aiModel = null);
    }
}
