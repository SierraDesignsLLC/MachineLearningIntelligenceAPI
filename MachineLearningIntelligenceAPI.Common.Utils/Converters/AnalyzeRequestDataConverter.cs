using MachineLearningIntelligenceAPI.DomainModels;
using MachineLearningIntelligenceAPI.DTOs;

namespace MachineLearningIntelligenceAPI.Common.Utils.Converters
{
    public class AnalyzeRequestDataConverter
    {
        /// <summary>
        /// Converts the request dto to the model
        /// </summary>
        public static AnalyzeRequest DtoToModel(AnalyzeRequestV1Dto dto)
        {
            var model = new AnalyzeRequest
            {
                InputStrings = dto.InputStrings,
                Culture = dto.Culture,
                Prompt = dto.Prompt,
            };

            return model;
        }
    }
}
