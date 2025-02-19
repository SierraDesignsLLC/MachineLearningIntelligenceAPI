using MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DomainModels;
using Microsoft.Extensions.Logging;

namespace MachineLearningIntelligenceAPI.DataAccess.Services
{
    public class AICommandService : IAICommandService
    {
        private readonly ILogger<AICommandService> _logger;
        private IAICommandRepository _aiCommandRepository { get; set; }
        public AICommandService(ILogger<AICommandService> logger, IAICommandRepository aiCommandRepository)
        {
            _logger = logger;
            _aiCommandRepository = aiCommandRepository;
        }

        /// <summary>
        /// Respond to passed in command
        /// </summary>
        public async Task<string> RespondToCommand(CommandRequest command, string aiModel = null)
        {
            string response = null;

            try
            {
                response = await _aiCommandRepository.RespondToCommand(command, aiModel);
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }
    }
}
