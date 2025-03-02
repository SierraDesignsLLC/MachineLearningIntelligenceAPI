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

            Assert.That(model, Is.Not.Null);
            Assert.That( model.AccountAutomationId, Is.EqualTo(accountAutomationId));
            Assert.That( model.ClientId, Is.EqualTo(clientId.ToString()));
            Assert.That( model.Name, Is.EqualTo(username));
            Assert.That( model.RefreshToken, Is.EqualTo(token));
            Assert.That( model.Status, Is.EqualTo(AccountAutomationStatusEnum.Active));
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

            Assert.That(dto, Is.Not.Null);
            Assert.That( dto.Id, Is.EqualTo(accountAutomationId));
            Assert.That( dto.ClientId, Is.EqualTo(clientId.ToString()));
            Assert.That( dto.AutomationType, Is.EqualTo(AccountAutomationTypeEnum.Reddit.ToString()));
            Assert.That( dto.DisplayName, Is.EqualTo(username));
            Assert.That( dto.RefreshToken, Is.EqualTo(token));
            Assert.That( dto.Verified, Is.EqualTo(true));
        }
    }
}
