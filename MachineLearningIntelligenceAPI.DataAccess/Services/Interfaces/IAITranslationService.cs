using MachineLearningIntelligenceAPI.DomainModels;

namespace MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces
{
    public interface IAITranslationService
    {
        /// <summary>
        /// Translation with AI with given input text, and language. Get response from AI as string
        /// </summary>
        public Task<string> TranslationWithAI(TranslationRequest conversation, string aiModel = null);
    }
}
