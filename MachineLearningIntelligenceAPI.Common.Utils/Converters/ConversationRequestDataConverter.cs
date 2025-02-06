using MachineLearningIntelligenceAPI.DomainModels;
using MachineLearningIntelligenceAPI.DTOs;

namespace MachineLearningIntelligenceAPI.Common.Utils.Converters
{
    public class ConversationRequestDataConverter
    {
        /// <summary>
        /// Converts the request dto to the model
        /// </summary>
        public static ConversationRequest DtoToModel(ConversationRequestV1Dto dto)
        {
            var model = new ConversationRequest
            {
                InputString = dto.InputString,
                Role = dto.Role,
                Context = dto.Context
            };

            return model;
        }
    }
}
