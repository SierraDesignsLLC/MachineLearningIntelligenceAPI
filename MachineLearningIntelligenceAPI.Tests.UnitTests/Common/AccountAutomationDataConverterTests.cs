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

            Assert.That(dto, Is.Not.Null);
            Assert.That( dto.Id, Is.EqualTo(accountAutomationId.ToString()));
            Assert.That( dto.AutomationType, Is.EqualTo(AccountAutomationTypeEnum.Reddit.ToString()));
            Assert.That( dto.AutomationStatus, Is.EqualTo(AccountAutomationStatusEnum.Active.ToString()));
            Assert.That( dto.DisplayName, Is.EqualTo(username));
            Assert.That( dto.Culture, Is.EqualTo(culture));
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
                Assert.That(dtos.ElementAt(i), Is.Not.Null);
                Assert.That(dtos.ElementAt(i).Id, Is.EqualTo(accountAutomationId.ToString()));
                Assert.That(dtos.ElementAt(i).AutomationType, Is.EqualTo(AccountAutomationTypeEnum.Reddit.ToString()));
                Assert.That(dtos.ElementAt(i).AutomationStatus, Is.EqualTo(AccountAutomationStatusEnum.Active.ToString()));
                Assert.That(dtos.ElementAt(i).DisplayName, Is.EqualTo(username));
                Assert.That(dtos.ElementAt(i).Culture, Is.EqualTo(culture));
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
                Assert.That(dtos.ElementAt(i), Is.Not.Null);
                Assert.That(dtos.ElementAt(i).Id, Is.EqualTo(accountAutomationId.ToString()));
                Assert.That(dtos.ElementAt(i).AutomationType, Is.EqualTo(AccountAutomationTypeEnum.Reddit.ToString()));
                Assert.That(dtos.ElementAt(i).AutomationStatus, Is.EqualTo(AccountAutomationStatusEnum.Active.ToString()));
                Assert.That(dtos.ElementAt(i).DisplayName, Is.EqualTo(username));
                Assert.That(dtos.ElementAt(i).Culture, Is.EqualTo(culture));
            }
        }
    }
}
