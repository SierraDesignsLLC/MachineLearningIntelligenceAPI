using MachineLearningIntelligenceAPI.Common.Enums;
using MachineLearningIntelligenceAPI.DomainModels.Reddit;
using MachineLearningIntelligenceAPI.DTOs.Reddit;

namespace MachineLearningIntelligenceAPI.Common.Utils.Converters.Reddit
{
    public class RedditUserAccountDataConverter
    {
        /// <summary>
        /// Converts the request dto to the model
        /// </summary>
        public static RedditUserAccount RequestToModel(RedditAccountAutomationV1Dto dto)
        {
            var redditUserAccount = new RedditUserAccount
            {
                AccountAutomationId = dto.Id,
                ClientId = dto.ClientId,
                Name = dto.DisplayName,
                RefreshToken = dto.RefreshToken,
                Status = dto.Verified ? AccountAutomationStatusEnum.Active : AccountAutomationStatusEnum.Unverified,
            };

            return redditUserAccount;
        }

        /// <summary>
        /// Converts the model to the request dto
        /// </summary>
        public static RedditAccountAutomationV1Dto ModelToV1Dto(RedditUserAccount model)
        {
            var redditUserAccount = new RedditAccountAutomationV1Dto
            {
                Id = model.AccountAutomationId,
                AutomationType = AccountAutomationTypeEnum.Reddit.ToString(),
                ClientId = model.ClientId,
                DisplayName = model.Name,
                RefreshToken = model.RefreshToken,
                Verified = model.Status == AccountAutomationStatusEnum.Active ? true : false,
            };

            return redditUserAccount;
        }
    }
}
