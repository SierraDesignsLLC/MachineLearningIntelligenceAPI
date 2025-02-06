using MachineLearningIntelligenceAPI.DomainModels.Reddit;
using MachineLearningIntelligenceAPI.DTOs.Reddit;
using MachineLearningIntelligenceAPI.Common.Enums;
using MachineLearningIntelligenceAPI.Common.Utils.Converters.Reddit;
using NUnit.Framework;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests.Common
{
    public class RedditUserAccountDataConverterTests
    {
        [Test]
        public void RequestToModelTest()
        {
            var accountAutomationId = Guid.NewGuid().ToString();
            var clientId = Guid.NewGuid();
            var token = Guid.NewGuid().ToString();
            var username = "username";

            var dto = new RedditAccountAutomationV1Dto
            {
                Id = accountAutomationId,
                ClientId = clientId.ToString(),
                DisplayName = username,
                RefreshToken = token,
                Verified = true,
            };

            var model = RedditUserAccountDataConverter.RequestToModel(dto);

            Assert.IsNotNull(model);
            Assert.AreEqual(accountAutomationId, model.AccountAutomationId);
            Assert.AreEqual(clientId.ToString(), model.ClientId);
            Assert.AreEqual(username, model.Name);
            Assert.AreEqual(token, model.RefreshToken);
            Assert.AreEqual(AccountAutomationStatusEnum.Active, model.Status);
        }

        [Test]
        public void ModelToV1DtoTest()
        {
            var accountAutomationId = Guid.NewGuid().ToString();
            var clientId = Guid.NewGuid();
            var token = Guid.NewGuid().ToString();
            var username = "username";

            var model = new RedditUserAccount
            {
                AccountAutomationId = accountAutomationId,
                ClientId = clientId.ToString(),
                Name = username,
                RefreshToken = token,
                Status = AccountAutomationStatusEnum.Active,
            };

            var dto = RedditUserAccountDataConverter.ModelToV1Dto(model);

            Assert.IsNotNull(dto);
            Assert.AreEqual(accountAutomationId, dto.Id);
            Assert.AreEqual(clientId.ToString(), dto.ClientId);
            Assert.AreEqual(AccountAutomationTypeEnum.Reddit.ToString(), dto.AutomationType);
            Assert.AreEqual(username, dto.DisplayName);
            Assert.AreEqual(token, dto.RefreshToken);
            Assert.AreEqual(true, dto.Verified);
        }
    }
}
