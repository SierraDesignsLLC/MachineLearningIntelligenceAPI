using MachineLearningIntelligenceAPI.DomainModels;
using MachineLearningIntelligenceAPI.DTOs;

namespace MachineLearningIntelligenceAPI.Common.Utils.Converters
{
    public class CommandRequestDataConverter
    {
        /// <summary>
        /// Converts the request dto to the model
        /// </summary>
        public static CommandRequest DtoToModel(CommandRequestV1Dto dto)
        {
            var model = new CommandRequest
            {
                InputString = dto.InputString,
                Culture = dto.Culture,
                Prompt = dto.Prompt,
            };

            return model;
        }
    }
}
