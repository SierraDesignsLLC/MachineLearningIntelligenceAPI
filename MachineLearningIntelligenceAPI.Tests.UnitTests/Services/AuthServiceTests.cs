using MachineLearningIntelligenceAPI.DataAccess.Services;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests.Services
{
    public class AuthServiceTests
    {
        private Mock<ILogger<AuthService>> _mockLogger;
        private Mock<IEncryptionService> _mockEncryptionService;

        private AuthService _service;

        [SetUp]
        public void Initialize()
        {
            _mockLogger = new Mock<ILogger<AuthService>>();
            _mockEncryptionService = new Mock<IEncryptionService>();

            _service = new AuthService(_mockLogger.Object, _mockEncryptionService.Object);
        }

        [TestCase("token")]
        [TestCase("owiefjowef.wefoijefwoji.wefojiioj")]
        [TestCase("...")]
        [TestCase("userid..token")]
        [Test]
        public void GetSessionFromSessionTokenMalformedTokenTest(string sessionToken)
        {
            //var ex = Assert.ThrowsAsync<Exception>(() => _service.GetSessionFromSessionToken(sessionToken));
            //Assert.That( ex.Message, Is.EqualTo(UnauthorizedString.UserAccountSessionUnauthorized));
        }

        [Test]
        public async Task GetSessionFromSessionTokenFailTest()
        {
            var sessionToken = "ea7be982-44c0-4f92-bf69-245dd3527b95.b93b0b78-c067-4387-9a7c-7cdae59adf21";
        }

        [Test]
        public async Task GetSessionFromSessionTokenTest()
        {
            var sessionToken = "ea7be982-44c0-4f92-bf69-245dd3527b95.b93b0b78-c067-4387-9a7c-7cdae59adf21";

            /*_mockUserAccountSessionService.Setup(mock => mock.GetSessionFromSessionToken("ea7be982-44c0-4f92-bf69-245dd3527b95", "b93b0b78-c067-4387-9a7c-7cdae59adf21"))
                .ReturnsAsync(new UserAccountSession());

            var result = await _service.GetSessionFromSessionToken(sessionToken);
            Assert.NotNull(result);*/
        }

        /*[Test]
        public async Task SignInUserAccountAndCreateOrGetSessionTest()
        {
            var userAccountId = Guid.NewGuid();
            var hashedPass = "fs4f9e8w989f7fw74==fwwf=fw";
            var userAccountLogin = new UserAccountLogin { Username = "testUser", PasswordEncrypted = "testPass" };
            var mockUserIdHashPass = new Tuple<Guid?, string>(userAccountId, hashedPass);
            var mockedUserAccountSession = new UserAccountSession();
            var userAccount = new UserAccount { Username = userAccountLogin.Username, PasswordEncrypted = "4g7e98984ef+3r+8", };

            _mockUserAccountDao.Setup(mock => mock.GetIdAndPasswordFromUsername(userAccountLogin.Username)).ReturnsAsync(mockUserIdHashPass);
            _mockEncryptionService.Setup(mock => mock.BcryptIsPasswordValid(userAccountLogin.PasswordEncrypted, hashedPass)).Returns(true);
            _mockUserAccountDao.Setup(mock => mock.GetUserAccountFromId(userAccountId)).ReturnsAsync(userAccount);
            _mockUserAccountSessionService.Setup(mock => mock.SignInUserAccountAndCreateSessionFromLogin(userAccountId, true)).ReturnsAsync(mockedUserAccountSession);

            var result = await _service.SignInUserAccountAndCreateSession(userAccountLogin);
            Assert.NotNull(result);
            Assert.NotNull(result.userAccount);
            Assert.NotNull(result.userSession);
        }

        [Test]
        public async Task SignInUserAccountAndCreateOrGetSessionNullIdTest()
        {
            string userAccountId = null;
            var hashedPass = "fs4f9e8w989f7fw74==fwwf=fw";
            var userAccountLogin = new UserAccountLogin { Username = "testUser", PasswordEncrypted = "testPass" };
            var mockUserIdHashPass = new Tuple<Guid?, string>(null, hashedPass);
            var mockedUserAccountSession = new UserAccountSession();

            _mockUserAccountDao.Setup(mock => mock.GetIdAndPasswordFromUsername(userAccountLogin.Username)).ReturnsAsync(mockUserIdHashPass);
            _mockEncryptionService.Setup(mock => mock.BcryptIsPasswordValid(userAccountLogin.PasswordEncrypted, hashedPass)).Returns(true);

            var ex = Assert.ThrowsAsync<Exception>(() => _service.SignInUserAccountAndCreateSession(userAccountLogin));
            Assert.That( ex.Message, Is.EqualTo(UnauthorizedString.UserAccountSessionUnauthorized));
            _mockLogger.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == ErrorInformationString.UserAccountNullId + " " + userAccountLogin.Username && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Test]
        public async Task SignInUserAccountAndCreateOrGetSessionPasswordNotMatchedTest()
        {
            var hashedPass = "fs4f9e8w989f7fw74==fwwf=fw";
            var userAccountLogin = new UserAccountLogin { Username = "testUser", PasswordEncrypted = "testPass" };
            var mockUserIdHashPass = new Tuple<Guid?, string>(null, hashedPass);
            var mockedUserAccountSession = new UserAccountSession();

            _mockUserAccountDao.Setup(mock => mock.GetIdAndPasswordFromUsername(userAccountLogin.Username)).ReturnsAsync(mockUserIdHashPass);
            _mockEncryptionService.Setup(mock => mock.BcryptIsPasswordValid(userAccountLogin.PasswordEncrypted, hashedPass)).Returns(false);

            var ex = Assert.ThrowsAsync<Exception>(() => _service.SignInUserAccountAndCreateSession(userAccountLogin));
            Assert.That( ex.Message, Is.EqualTo(UnauthorizedString.UserAccountUnauthorized));
        }

        [Test]
        public async Task AuthenticateUserAccountWithSessionTokenTest()
        {
            var userAccountId = Guid.NewGuid();
            var sessionToken = "b93b0b78-c067-4387-9a7c-7cdae59adf21";
            var returnedUserAccountSession = new UserAccountSession();
            _mockUserAccountSessionService.Setup(mock => mock.SignInUserAccountFromIdTokenAndUpdateExpiration(userAccountId, new Guid(sessionToken))).ReturnsAsync(returnedUserAccountSession);

            var result = await _service.AuthenticateUserAccountWithSessionToken(userAccountId, new Guid(sessionToken));
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task AuthenticateUserAccountWithSessionTokenFailTest()
        {
            var userAccountId = Guid.NewGuid();
            var sessionToken = "b93b0b78-c067-4387-9a7c-7cdae59adf21";
            _mockUserAccountSessionService.Setup(mock => mock.SignInUserAccountFromIdTokenAndUpdateExpiration(userAccountId, new Guid(sessionToken))).ReturnsAsync((UserAccountSession)null);

            var ex = Assert.ThrowsAsync<Exception>(() => _service.AuthenticateUserAccountWithSessionToken(userAccountId, new Guid(sessionToken)));
            Assert.That( ex.Message, Is.EqualTo(UnauthorizedString.UserAccountSessionUnauthorized));
        }

        [TestCase("token")]
        [TestCase("owiefjowef.wefoijefwoji.wefojiioj")]
        [TestCase("...")]
        [TestCase("userid..token")]
        [Test]
        public void RevokeSessionFromSessionTokenMalformedTokenTest(string sessionToken)
        {
            var ex = Assert.ThrowsAsync<Exception>(() => _service.RevokeSessionFromSessionToken(sessionToken));
            Assert.That( ex.Message, Is.EqualTo(UnauthorizedString.UserAccountSessionUnauthorized));
        }

        [Test]
        public async Task RevokeSessionFromSessionTokenTest()
        {
            var sessionToken = "ea7be982-44c0-4f92-bf69-245dd3527b95.b93b0b78-c067-4387-9a7c-7cdae59adf21";

            await _service.RevokeSessionFromSessionToken(sessionToken);

            _mockUserAccountSessionService.Verify(mock => mock.SignOutUserAccountFromIdToken(new Guid("ea7be982-44c0-4f92-bf69-245dd3527b95"),
                new Guid("b93b0b78-c067-4387-9a7c-7cdae59adf21")), Times.Once);
        }*/
    }
}