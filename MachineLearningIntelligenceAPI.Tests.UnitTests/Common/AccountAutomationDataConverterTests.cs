using NUnit.Framework;
using MachineLearningIntelligenceAPI.Common.Enums;
using MachineLearningIntelligenceAPI.Common.Utils.Converters;
using MachineLearningIntelligenceAPI.DomainModels;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests.Common
{
    public class AccountAutomationDataConverterTests
    {
        [Test]
        public void ModelToV1DtoTest()
        {
            var accountAutomationId = Guid.NewGuid();
            var userAccountId = Guid.NewGuid().ToString();
            var clientId = Guid.NewGuid();
            var token = Guid.NewGuid().ToString();
            var username = "username";
            var culture = "en-US";

            var model = new AccountAutomation
            {
                Id = accountAutomationId,
                UserAccountId = userAccountId,
                ClientId = clientId.ToString(),
                DisplayName = username,
                RefreshToken = token,
                Culture = culture,
                AutomationType = (int)AccountAutomationTypeEnum.Reddit,
                AutomationStatus = (int)AccountAutomationStatusEnum.Active,
            };

            var dto = AccountAutomationDataConverter.ModelToV1Dto(model);

            Assert.IsNotNull(dto);
            Assert.AreEqual(accountAutomationId.ToString(), dto.Id);
            Assert.AreEqual(AccountAutomationTypeEnum.Reddit.ToString(), dto.AutomationType);
            Assert.AreEqual(AccountAutomationStatusEnum.Active.ToString(), dto.AutomationStatus);
            Assert.AreEqual(username, dto.DisplayName);
            Assert.AreEqual(culture, dto.Culture);
        }

        [Test]
        public void ModelToV1DtoListTest()
        {
            var accountAutomationId = Guid.NewGuid();
            var userAccountId = Guid.NewGuid().ToString();
            var clientId = Guid.NewGuid();
            var token = Guid.NewGuid().ToString();
            var username = "username";
            var culture = "en-US";

            var model = new AccountAutomation
            {
                Id = accountAutomationId,
                UserAccountId = userAccountId,
                ClientId = clientId.ToString(),
                DisplayName = username,
                RefreshToken = token,
                Culture = culture,
                AutomationType = (int)AccountAutomationTypeEnum.Reddit,
                AutomationStatus = (int)AccountAutomationStatusEnum.Active,
            };
            var model2 = new AccountAutomation
            {
                Id = accountAutomationId,
                UserAccountId = userAccountId,
                ClientId = clientId.ToString(),
                DisplayName = username,
                RefreshToken = token,
                Culture = culture,
                AutomationType = (int)AccountAutomationTypeEnum.Reddit,
                AutomationStatus = (int)AccountAutomationStatusEnum.Active,
            };
            var model3 = new AccountAutomation
            {
                Id = accountAutomationId,
                UserAccountId = userAccountId,
                ClientId = clientId.ToString(),
                DisplayName = username,
                RefreshToken = token,
                Culture = culture,
                AutomationType = (int)AccountAutomationTypeEnum.Reddit,
                AutomationStatus = (int)AccountAutomationStatusEnum.Active,
            };

            List<AccountAutomation> list = new List<AccountAutomation> { model, model2, model3 };
            var dtos = AccountAutomationDataConverter.ModelToV1Dto(list);

            for (int i = 0; i < list.Count || i < dtos.Count; i++)
            {
                Assert.IsNotNull(dtos.ElementAt(i));
                Assert.AreEqual(accountAutomationId.ToString(), dtos.ElementAt(i).Id);
                Assert.AreEqual(AccountAutomationTypeEnum.Reddit.ToString(), dtos.ElementAt(i).AutomationType);
                Assert.AreEqual(AccountAutomationStatusEnum.Active.ToString(), dtos.ElementAt(i).AutomationStatus);
                Assert.AreEqual(username, dtos.ElementAt(i).DisplayName);
                Assert.AreEqual(culture, dtos.ElementAt(i).Culture);
            }
        }

        [Test]
        public void ModelToV1DtoListNullTest()
        {
            var accountAutomationId = Guid.NewGuid();
            var userAccountId = Guid.NewGuid().ToString();
            string clientId = null;
            string token = null;
            string username = null;
            string culture = null;

            var model = new AccountAutomation
            {
                Id = accountAutomationId,
                UserAccountId = userAccountId,
                ClientId = clientId?.ToString(),
                DisplayName = username,
                RefreshToken = token,
                Culture = culture,
                AutomationType = (int)AccountAutomationTypeEnum.Reddit,
            };
            var model2 = new AccountAutomation
            {
                Id = accountAutomationId,
                UserAccountId = userAccountId,
                ClientId = clientId?.ToString(),
                DisplayName = username,
                RefreshToken = token,
                Culture = culture,
                AutomationType = (int)AccountAutomationTypeEnum.Reddit,
            };
            var model3 = new AccountAutomation
            {
                Id = accountAutomationId,
                UserAccountId = userAccountId,
                ClientId = clientId?.ToString(),
                DisplayName = username,
                RefreshToken = token,
                Culture = culture,
                AutomationType = (int)AccountAutomationTypeEnum.Reddit,
            };

            List<AccountAutomation> list = new List<AccountAutomation> { model, model2, model3 };
            var dtos = AccountAutomationDataConverter.ModelToV1Dto(list);

            for (int i = 0; i < list.Count || i < dtos.Count; i++)
            {
                Assert.IsNotNull(dtos.ElementAt(i));
                Assert.AreEqual(accountAutomationId.ToString(), dtos.ElementAt(i).Id);
                Assert.AreEqual(AccountAutomationTypeEnum.Reddit.ToString(), dtos.ElementAt(i).AutomationType);
                Assert.AreEqual(AccountAutomationStatusEnum.Active.ToString(), dtos.ElementAt(i).AutomationStatus);
                Assert.AreEqual(username, dtos.ElementAt(i).DisplayName);
                Assert.AreEqual(culture, dtos.ElementAt(i).Culture);
            }
        }
    }
}
