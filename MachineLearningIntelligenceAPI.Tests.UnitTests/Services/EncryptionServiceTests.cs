using MachineLearningIntelligenceAPI.DataAccess.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests.Services
{
    public class EncryptionServiceTests
    {
        private Mock<ILogger<EncryptionService>> _mockLogger;

        private EncryptionService _service;

        [SetUp]
        public void Initialize()
        {
            _mockLogger = new Mock<ILogger<EncryptionService>>();

            _service = new EncryptionService(_mockLogger.Object);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("password")]
        [TestCase("  password  ")]
        [TestCase("password 123")]
        [TestCase("password 123 /?/!@#$%^&*()-=+_åp½¶˜Ø╪")]
        [Test]
        public void BcryptEncryptPasswordTest(string password)
        {
            var unhashedPass = password;
            if (unhashedPass == null)
            {
                var ex = Assert.Throws<ArgumentNullException>(() => _service.BcryptEncryptPassword(unhashedPass));
                Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'inputKey')"));
                Assert.Pass();
                return;
            }
            var result = _service.BcryptEncryptPassword(unhashedPass);

            Assert.That(unhashedPass, Is.Not.EqualTo(result));
        }

        [TestCase("", "$2a$11$0ddEthscZdOQnMnzmpUcZuSaIyjvKxJ/OfIcNkc6G8CLGb3sWy34m", false)]
        [TestCase(null, "", false)]
        [TestCase("password", "$2a$11$o2sId1/XJpTL9.ucLzQ0n.kwlvSFhZKev4gc.Sv4u6kHJecbNo/Iu", true)]
        [TestCase("  password  ", "$2a$11$UxpkxxFnVMSvf2Gk4wRtd.T2jDA094MzdNZx2J/zxXn1w7kSqjw1W", true)]
        [TestCase("password 123", "$2a$11$/ualeMFh9JaIlWpYMD9TNuExm0JNbs.q7SFrFhOk6OZvwTjX4p932", true)]
        [TestCase("password 123 /?/!@#$%^&*()-=+_åp½¶˜Ø╪", "$2a$11$qYxFIB5ArUWB6sGBnbk9yeQPMNefoRS3wbDXWcpIR.NuE8dwpX.Kq", true)]
        [TestCase(" password", "$2a$11$o2sId1/XJpTL9.ucLzQ0n.kwlvSFhZKev4gc.Sv4u6kHJecbNo/Iu", false)]
        [TestCase(" password  ", "$2a$11$UxpkxxFnVMSvf2Gk4wRtd.T2jDA094MzdNZx2J/zxXn1w7kSqjw1W", false)]
        [TestCase("password123", "$2a$11$/ualeMFh9JaIlWpYMD9TNuExm0JNbs.q7SFrFhOk6OZvwTjX4p932", false)]
        [TestCase("password123 /?/!@#$%^&*()-=+_åp½¶˜Ø╪", "$2a$11$qYxFIB5ArUWB6sGBnbk9yeQPMNefoRS3wbDXWcpIR.NuE8dwpX.Kq", false)]
        [Test]
        public void BcryptIsPasswordValid(string password, string hash, bool validPassword)
        {
            var result = _service.BcryptIsPasswordValid(password, hash);

            Assert.That(result, Is.EqualTo(validPassword));
        }

        [TestCase(32, 44)]
        [Test]
        public void GenerateSaltWithLength(int length, int expectedLength)
        {
            var result = _service.GenerateSaltWithLength(length);

            Assert.That(result.Length, Is.EqualTo(expectedLength));
        }
    }
}
