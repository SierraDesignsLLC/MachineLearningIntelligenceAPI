using MachineLearningIntelligenceAPI.DomainModels;
using MachineLearningIntelligenceAPI.DTOs;

namespace MachineLearningIntelligenceAPI.Common.Utils.Converters
{
    public class TranslationRequestDataConverter
    {
        /// <summary>
        /// Converts the request dto to the model
        /// </summary>
        public static TranslationRequest DtoToModel(TranslationRequestV1Dto dto)
        {
            var model = new TranslationRequest
            {
                InputStrings = dto.InputStrings,
                Culture = dto.Culture,
            };

            return model;
        }

    }
}
