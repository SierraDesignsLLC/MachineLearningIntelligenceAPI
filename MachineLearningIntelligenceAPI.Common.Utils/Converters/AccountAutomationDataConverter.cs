using MachineLearningIntelligenceAPI.Common.Enums;
using MachineLearningIntelligenceAPI.DomainModels;
using MachineLearningIntelligenceAPI.DTOs;

namespace MachineLearningIntelligenceAPI.Common.Utils.Converters
{
    public class AccountAutomationDataConverter
    {
        public static AccountAutomationV1Dto ModelToV1Dto(AccountAutomation model)
        {
            var dto = new AccountAutomationV1Dto
            {
                Id = model.Id.ToString(),
                AutomationType = ((AccountAutomationTypeEnum)model.AutomationType).ToString(),
                AutomationStatus = ((AccountAutomationStatusEnum)model.AutomationStatus).ToString(),
                DisplayName = model.DisplayName,
                Culture = model.Culture?.ToString(),
            };
            return dto;
        }

        public static List<AccountAutomationV1Dto> ModelToV1Dto(List<AccountAutomation> model)
        {
            var list = new List<AccountAutomationV1Dto>();
            foreach (var item in model)
            {
                list.Add(ModelToV1Dto(item));
            }
            return list;
        }
    }
}
