using MachineLearningIntelligenceAPI.DomainModels;
using MachineLearningIntelligenceAPI.DTOs;

namespace MachineLearningIntelligenceAPI.Common.Utils.Converters
{
    public class SocialMediaContentDataConverter
    {
        /// <summary>
        /// Converts the request dto to the model
        /// </summary>
        public static SocialMediaContent RequestToModel(SocialMediaContentV1Dto dto)
        {
            var model = new SocialMediaContent
            {
                Title = dto.Title,
                Message = dto.Message,
                Media = RequestToModel(dto.Media),
            };

            return model;
        }

        /// <summary>
        /// Converts the request dto to the model
        /// </summary>
        public static List<Media> RequestToModel(List<MediaV1Dto> dto)
        {
            if (dto == null)
            {
                return null;
            }
            var model = new List<Media>();
            foreach (var item in dto)
            {
                model.Add(RequestToModel(item));
            }

            return model;
        }

        /// <summary>
        /// Converts the request dto to the model
        /// </summary>
        public static Media RequestToModel(MediaV1Dto dto)
        {
            var model = new Media
            {
                Content = dto.Content,
                Type = dto.Type,
                EncodingFormat = dto.EncodingFormat,
                FileName = dto.FileName,
                Caption = dto.Caption,
                Link = dto.Link,
            };

            return model;
        }
    }
}
