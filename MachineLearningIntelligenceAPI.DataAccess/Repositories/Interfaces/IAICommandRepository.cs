using MachineLearningIntelligenceAPI.DomainModels;

namespace MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces
{
    public interface IAICommandRepository
    {
        /// <summary>
        /// Respond to passed in command
        /// </summary>
        public Task<string> RespondToCommand(CommandRequest analysis, string aiModel = null);
    }
}
