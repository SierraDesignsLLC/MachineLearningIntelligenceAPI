using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Net;
using MachineLearningIntelligenceAPI.DomainModels;
using MachineLearningIntelligenceAPI.DataAccess.Services.Interfaces;
using MachineLearningIntelligenceAPI.DataAccess;
using MachineLearningIntelligenceAPI.Common.StringConstants;
using MachineLearningIntelligenceAPI.Common.Auth;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests.Controllers
{
    /// <summary>
    /// Tests for the controller base
    /// </summary>
    public class ControllerBaseTests
    {
        public RequestSessionInformation RequestSessionInformation { get; set; } = new RequestSessionInformation();

        private Mock<ILogger<MachineLearningIntelligenceAPI.Controllers.ControllerBase>> _mockLogger;
        private Mock<IAuthService> _mockAuthService;

        private MachineLearningIntelligenceAPI.Controllers.ControllerBase _controller;

        [SetUp]
        public void Initialize()
        {
            _mockLogger = new Mock<ILogger<MachineLearningIntelligenceAPI.Controllers.ControllerBase>>();
            _mockAuthService = new Mock<IAuthService>();

            var context = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            RequestSessionInformation.Authentication = null;
            RequestSessionInformation.RequestUserId = null;
            RequestSessionInformation.Permissions = null;
            RequestSessionInformation.HasValidatedAuthentication = false;
            _controller = new MachineLearningIntelligenceAPI.Controllers.ControllerBase(RequestSessionInformation, _mockLogger.Object, _mockAuthService.Object);
            _controller.ControllerContext = context;
        }

        [Test]
        public void GenerateHttpResponseFromExceptionTest()
        {
            FieldInfo[] fields = typeof(BadRequestString).GetFields();
            foreach (FieldInfo field in fields)
            {
                var message = field.GetValue(null).ToString();
                var result = _controller.GenerateHttpResponseFromException(new Exception(message));
                Assert.IsNotNull(result);
                Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
                Assert.AreEqual(message, result.Value);
            }

            fields = typeof(UnauthorizedString).GetFields();
            foreach (FieldInfo field in fields)
            {
                var message = field.GetValue(null).ToString();
                var result = _controller.GenerateHttpResponseFromException(new Exception(message));
                Assert.IsNotNull(result);
                Assert.AreEqual((int)HttpStatusCode.Unauthorized, result.StatusCode);
                Assert.AreEqual(message, result.Value);
            }

            fields = typeof(ForbiddenString).GetFields();
            foreach (FieldInfo field in fields)
            {
                var message = field.GetValue(null).ToString();
                var result = _controller.GenerateHttpResponseFromException(new Exception(message));
                Assert.IsNotNull(result);
                Assert.AreEqual((int)HttpStatusCode.Forbidden, result.StatusCode);
                Assert.AreEqual(message, result.Value);
            }

            fields = typeof(NotFoundString).GetFields();
            foreach (FieldInfo field in fields)
            {
                var message = field.GetValue(null).ToString();
                var result = _controller.GenerateHttpResponseFromException(new Exception(message));
                Assert.IsNotNull(result);
                Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
                Assert.AreEqual(message, result.Value);
            }

            fields = typeof(TooManyRequestsString).GetFields();
            foreach (FieldInfo field in fields)
            {
                var message = field.GetValue(null).ToString();
                var result = _controller.GenerateHttpResponseFromException(new Exception(message));
                Assert.IsNotNull(result);
                Assert.AreEqual((int)HttpStatusCode.TooManyRequests, result.StatusCode);
                Assert.AreEqual(message, result.Value);
            }

            var result500 = _controller.GenerateHttpResponseFromException(new Exception(""));
            Assert.IsNotNull(result500);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result500.StatusCode);
        }

        [TestCase(IACStrings.RedditService)]
        [Test]
        public async Task AuthorizeWithIACTest(string iacName)
        {
            var userAccountId = Guid.NewGuid();
            var sessionToken = Guid.NewGuid();
            var userPermissions = new List<UserAccountPermission> { new UserAccountPermission { PermissionTypeId = 1, PermissionName = iacName},
                new UserAccountPermission { PermissionTypeId = 1, PermissionName = "" }, new UserAccountPermission { PermissionTypeId = 1, PermissionName = "redditservice" },
                new UserAccountPermission { PermissionTypeId = 3, PermissionName = iacName }, };
            RequestSessionInformation.Authentication = sessionToken;
            RequestSessionInformation.RequestUserId = userAccountId;
            RequestSessionInformation.Permissions = userPermissions;

            _controller.HttpContext.Request.Headers.Add("Authorization", "Bearer " + userAccountId + "." + sessionToken);
            var requiredPermissions = new List<(string permissionType, string permissionName)> { ("todosetup", IACStrings.RedditService), };

            await _controller.AuthorizePermissions(requiredPermissions);
        }

        [TestCase(FRPStrings.RedditPosting)]
        [Test]
        public async Task AuthorizeWithFRPTest(string frpName)
        {
            var userAccountId = Guid.NewGuid();
            var sessionToken = Guid.NewGuid();
            var userPermissions = new List<UserAccountPermission> { new UserAccountPermission { PermissionTypeId = 1, PermissionName = frpName},
                new UserAccountPermission { PermissionTypeId = 1, PermissionName = "" }, new UserAccountPermission { PermissionTypeId = 1, PermissionName = "redditservice" },
                new UserAccountPermission { PermissionTypeId = 3, PermissionName = frpName }, };
            RequestSessionInformation.Authentication = sessionToken;
            RequestSessionInformation.RequestUserId = userAccountId;
            RequestSessionInformation.Permissions = userPermissions;

            _controller.HttpContext.Request.Headers.Add("Authorization", "Bearer " + userAccountId + "." + sessionToken);
            var requiredPermissions = new List<(string permissionType, string permissionName)> { ("todosetup", FRPStrings.RedditPosting), };

            await _controller.AuthorizePermissions(requiredPermissions);
        }

        [TestCase(IACStrings.RedditService)]
        [Test]
        public async Task AuthorizeWithIACFailTest(string iacName)
        {
            var userAccountId = Guid.NewGuid();
            var sessionToken = Guid.NewGuid();
            var userPermissions = new List<UserAccountPermission> {
                new UserAccountPermission { PermissionTypeId = 1, PermissionName = "" }, new UserAccountPermission { PermissionTypeId = 1, PermissionName = "redditservice" },
                new UserAccountPermission { PermissionTypeId = 3, PermissionName = iacName }, };
            RequestSessionInformation.Authentication = sessionToken;
            RequestSessionInformation.RequestUserId = userAccountId;
            RequestSessionInformation.Permissions = userPermissions;

            _controller.HttpContext.Request.Headers.Add("Authorization", "Bearer " + userAccountId + "." + sessionToken);
            var requiredPermissions = new List<(string permissionType, string permissionName)> { ("todosetup", IACStrings.RedditService), };

            var ex = Assert.ThrowsAsync<Exception>(() => _controller.AuthorizePermissions(requiredPermissions));
            Assert.AreEqual(ForbiddenString.InfrastructureForbidden, ex.Message);
        }

        [TestCase(FRPStrings.RedditPosting)]
        [Test]
        public async Task AuthorizeWithFRPFailTest(string frpName)
        {
            var userAccountId = Guid.NewGuid();
            var sessionToken = Guid.NewGuid();
            var userPermissions = new List<UserAccountPermission> {
                new UserAccountPermission { PermissionTypeId = 1, PermissionName = "" },
                new UserAccountPermission { PermissionTypeId = 1, PermissionName = "redditservice" },
                new UserAccountPermission { PermissionTypeId = 2, PermissionName = frpName }, };
            RequestSessionInformation.Authentication = sessionToken;
            RequestSessionInformation.RequestUserId = userAccountId;
            RequestSessionInformation.Permissions = userPermissions;

            _controller.HttpContext.Request.Headers.Add("Authorization", "Bearer " + userAccountId + "." + sessionToken);
            var requiredPermissions = new List<(string permissionType, string permissionName)> { ("todosetup", FRPStrings.RedditPosting), };

            var ex = Assert.ThrowsAsync<Exception>(() => _controller.AuthorizePermissions(requiredPermissions));
            Assert.AreEqual(ForbiddenString.FeatureForbidden, ex.Message);
        }

        [Test]
        public async Task AuthorizeWithIACNoPermissionsFailTest()
        {
            var userAccountId = Guid.NewGuid();
            var sessionToken = Guid.NewGuid();

            _controller.HttpContext.Request.Headers.Add("Authorization", "Bearer " + userAccountId + "." + sessionToken);
            RequestSessionInformation.Authentication = sessionToken;
            RequestSessionInformation.RequestUserId = userAccountId;
            var requiredPermissions = new List<(string permissionType, string permissionName)> { ("todosetup", IACStrings.RedditService), };

            var ex = Assert.ThrowsAsync<Exception>(() => _controller.AuthorizePermissions(requiredPermissions));
            Assert.AreEqual(ForbiddenString.InfrastructureForbidden, ex.Message);
        }

        [Test]
        public async Task AuthorizeWithFRPNoPermissionsFailTest()
        {
            var userAccountId = Guid.NewGuid();
            var sessionToken = Guid.NewGuid();

            _controller.HttpContext.Request.Headers.Add("Authorization", "Bearer " + userAccountId + "." + sessionToken);
            RequestSessionInformation.Authentication = sessionToken;
            RequestSessionInformation.RequestUserId = userAccountId;
            var requiredPermissions = new List<(string permissionType, string permissionName)> { ("todosetup", FRPStrings.RedditPosting), };

            var ex = Assert.ThrowsAsync<Exception>(() => _controller.AuthorizePermissions(requiredPermissions));
            Assert.AreEqual(ForbiddenString.InfrastructureForbidden, ex.Message);
        }
    }
}
