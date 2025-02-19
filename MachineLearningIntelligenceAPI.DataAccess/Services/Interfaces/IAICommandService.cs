using MachineLearningIntelligenceAPI.DomainModels;

namespace MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces
{
    public interface IAICommandService
    {
        /// <summary>
        /// Respond to passed in command
        /// </summary>
        public Task<string> RespondToCommand(CommandRequest command, string aiModel = null);
    }
}
