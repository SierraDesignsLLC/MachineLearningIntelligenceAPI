using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess;
using MachineLearningIntelligenceAPI.Common;
using MachineLearningIntelligenceAPI.Common.StringConstants;
using MachineLearningIntelligenceAPI.Common.Auth;
using MachineLearningIntelligenceAPI.Middleware;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests
{
    public class RequestSessionInformationMiddlewareTests
    {
        public RequestSessionInformation RequestSessionInformation { get; set; } = new RequestSessionInformation();

        private Mock<ILogger<RequestSessionInformationMiddleware>> _mockLogger;
        private Mock<IAuthService> _mockAuthService;

        private RequestSessionInformationMiddleware _middleware;

        [SetUp]
        public void Initialize()
        {
            _mockLogger = new Mock<ILogger<RequestSessionInformationMiddleware>>();
            _mockAuthService = new Mock<IAuthService>();

            RequestSessionInformation.Authentication = null;
            RequestSessionInformation.RequestUserId = null;
            RequestSessionInformation.Permissions = null;
            RequestSessionInformation.HasValidatedAuthentication = false;

            _middleware = new RequestSessionInformationMiddleware(RequestSessionInformation, _mockLogger.Object, _mockAuthService.Object);
        }

        [TestCase("ea7be982-44c0-4f92-bf69-245dd3527b95.b93b0b78-c067-4387-9a7c-7cdae59adf21", "ea7be982-44c0-4f92-bf69-245dd3527b95", "b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase("ea7be98244c04f92bf69245dd3527b95.b93b0b78c06743879a7c7cdae59adf21", "ea7be98244c04f92bf69245dd3527b95", "b93b0b78c06743879a7c7cdae59adf21")]
        [Test]
        public void GetTokenStringsTest(string token, string userAccountId, string sessionToken)
        {
            var result = _middleware.GetTokenStrings(token);
            Assert.That(result, Is.Not.Null);
            Assert.That(result[0], Is.EqualTo(userAccountId));
            Assert.That(result[1], Is.EqualTo(sessionToken));
        }

        [TestCase(".ea7be982-44c0-4f92-bf69-245dd3527b95.b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase("ea7be98244c04f92bf69245dd3527b95.b93b0b78c06743879a7c7cdae59adf21.")]
        [TestCase("ea7be98244c04f92bf69245dd3527b95.b93b0b78c06743879a7c7cdae59adf21.asdf")]
        [Test]
        public void GetTokenStringsFailTest(string token)
        {
            var ex = Assert.Throws<Exception>(() => _middleware.GetTokenStrings(token));
            Assert.That(ex.Message, Is.EqualTo(UnauthorizedString.UserAccountSessionUnauthorized));
        }

        [TestCase(AuthPolicyStrings.ActiveSession, "")]
        [TestCase(AuthPolicyStrings.ActiveSession, null)]
        [TestCase(AuthPolicyStrings.ActiveSession, "ea7be982-44c0-4f92-bf69-245dd3527b95b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase(AuthPolicyStrings.ActiveSession, "ea7be982-44c0-4f92-bf69-245dd3527b95.b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase(AuthPolicyStrings.ActiveSession, "Bearerea7be982-44c0-4f92-bf69-245dd3527b95b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase(AuthPolicyStrings.ActiveSession, "Bearerea7be982-44c0-4f92-bf69-245dd3527b95.b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase(AuthPolicyStrings.ActiveSession, "Bearer ea7be982-44c0-4f92-bf69-245dd3527b95b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase(AuthPolicyStrings.ActiveSession, "Bearerea7be982-44c0-4f92-bf69-245dd3527b95.")]
        [TestCase(AuthPolicyStrings.ActiveSession, "Bearerea.b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase(AuthPolicyStrings.ActiveSession, "Bearer ea7be982-44c0-4f92-bf69-245dd3527b95.")]
        [TestCase(AuthPolicyStrings.ActiveSession, "Bearer .b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase(AuthPolicyStrings.ActiveSession, "Bearer ea7be982-44c0-4f92-bf69-245dd3527b.b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [Test]
        public async Task AuthorizeWithPolicyMalformedTokenFailTest(string policyName, string fullToken)
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Add("Authorization", fullToken);
            var ex = Assert.ThrowsAsync<Exception>(() => _middleware.SetRequestSessionInformation(context.Request));
            Assert.That(ex.Message, Is.EqualTo(UnauthorizedString.UserAccountSessionUnauthorized));
            Assert.That(RequestSessionInformation.RequestUserId, Is.EqualTo(null));
            Assert.That(RequestSessionInformation.Authentication, Is.EqualTo(null));
            Assert.That(RequestSessionInformation.HasValidatedAuthentication, Is.EqualTo(false));
        }

        [TestCase(AuthPolicyStrings.ActiveSession, "ea7be982-44c0-4f92-bf69-245dd3527b95", "b93b0b78-c067-4387-9a7c-7cdae59adf21")]
        [TestCase(AuthPolicyStrings.ActiveSession, "ea7be98244c04f92bf69245dd3527b95", "b93b0b78c06743879a7c7cdae59adf21")]
        [Test]
        public async Task AuthorizeWithPolicyTest(string policyName, string userAccountId, string sessionToken)
        {
            var fullToken = $"Bearer {userAccountId}.{sessionToken}";
            var context = new DefaultHttpContext();
            context.Request.Headers.Add("Authorization", fullToken);
            await _middleware.SetRequestSessionInformation(context.Request);
        }

        [Test]
        public async Task SystemSetRequestSessionInformationTest()
        {
            var fullToken = $"Bearer {Constants.SystemUserAccountGuid}";
            var context = new DefaultHttpContext();
            context.Request.Headers.Add("Authorization", fullToken);
            await _middleware.SetRequestSessionInformation(context.Request);
            Assert.That(RequestSessionInformation.RequestUserId, Is.EqualTo(null));
            Assert.That(RequestSessionInformation.Authentication, Is.EqualTo(null));
            Assert.That(RequestSessionInformation.Permissions, Is.EqualTo(null));
        }
    }
}
