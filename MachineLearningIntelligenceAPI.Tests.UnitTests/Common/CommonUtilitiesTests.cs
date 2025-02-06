using NUnit.Framework;
using MachineLearningIntelligenceAPI.Common.Enums;
using MachineLearningIntelligenceAPI.Common.Utils;

namespace MachineLearningIntelligenceAPI.Tests.UnitTests.Common
{
    public class CommonUtilitiesTests
    {
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa", true)]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-asdffwefw", true)]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaa", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        [Test]
        public async Task IsTemporaryId(string id, bool expectedReturn)
        {
            var result = CommonUtilities.IsTemporaryId(id);
            Assert.AreEqual(expectedReturn, result);
        }

        [TestCase(null, false, false, false)]
        [TestCase(null, true, false, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.Unknown, false, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.Unran, false, false, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.RanUnsuccessfully, false, false, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.InProgress, false, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.InProgress, false, true, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.ManuallyRetriedUnsuccessfully, false, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.FailedToPersistPayloadRemotely, false, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.RanSuccessfully, false, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.Unknown, true, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.Unran, true, false, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.RanUnsuccessfully, true, false, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.InProgress, true, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.InProgress, true, true, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.ManuallyRetriedUnsuccessfully, true, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.FailedToPersistPayloadRemotely, true, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.RanSuccessfully, true, false, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.Unknown, false, true, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.Unran, false, true, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.RanUnsuccessfully, false, true, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.InProgress, false, true, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.ManuallyRetriedUnsuccessfully, false, true, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.FailedToPersistPayloadRemotely, false, true, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.RanSuccessfully, false, true, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.Unknown, true, true, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.Unran, true, true, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.RanUnsuccessfully, true, true, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.InProgress, true, true, true)]
        [TestCase((int)AccountAutomationJobStatusEnum.ManuallyRetriedUnsuccessfully, true, true, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.FailedToPersistPayloadRemotely, true, true, false)]
        [TestCase((int)AccountAutomationJobStatusEnum.RanSuccessfully, true, true, false)]
        [Test]
        public void IsValidAccountAutomationJobStatusTest(int? status, bool allowNull, bool allowInProgress, bool expectedResult)
        {
            var result = CommonUtilities.IsValidAccountAutomationJobStatus(status, allowNull, allowInProgress);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(null, false, false, false)]
        [TestCase(null, true, false, true)]
        [TestCase((int)EngagementJobStatusEnum.Unknown, false, false, false)]
        [TestCase((int)EngagementJobStatusEnum.Unran, false, false, true)]
        [TestCase((int)EngagementJobStatusEnum.RanUnsuccessfully, false, false, true)]
        [TestCase((int)EngagementJobStatusEnum.InProgress, false, false, false)]
        [TestCase((int)EngagementJobStatusEnum.InProgress, false, true, true)]
        [TestCase((int)EngagementJobStatusEnum.Expired, false, false, false)]
        [TestCase((int)EngagementJobStatusEnum.RetriedUnsuccessfully, false, false, true)]
        [TestCase((int)EngagementJobStatusEnum.AccountAutomationReconnectionNeeded, false, false, false)]
        [TestCase((int)EngagementJobStatusEnum.RanSuccessfully, false, false, true)]
        [TestCase((int)EngagementJobStatusEnum.Unknown, true, false, false)]
        [TestCase((int)EngagementJobStatusEnum.Unran, true, false, true)]
        [TestCase((int)EngagementJobStatusEnum.RanUnsuccessfully, true, false, true)]
        [TestCase((int)EngagementJobStatusEnum.InProgress, true, false, false)]
        [TestCase((int)EngagementJobStatusEnum.InProgress, true, true, true)]
        [TestCase((int)EngagementJobStatusEnum.Expired, true, false, false)]
        [TestCase((int)EngagementJobStatusEnum.RetriedUnsuccessfully, true, false, true)]
        [TestCase((int)EngagementJobStatusEnum.AccountAutomationReconnectionNeeded, true, false, false)]
        [TestCase((int)EngagementJobStatusEnum.RanSuccessfully, true, false, true)]
        [TestCase((int)EngagementJobStatusEnum.Unknown, false, true, false)]
        [TestCase((int)EngagementJobStatusEnum.Unran, false, true, true)]
        [TestCase((int)EngagementJobStatusEnum.RanUnsuccessfully, false, true, true)]
        [TestCase((int)EngagementJobStatusEnum.InProgress, false, true, true)]
        [TestCase((int)EngagementJobStatusEnum.Expired, false, true, false)]
        [TestCase((int)EngagementJobStatusEnum.RetriedUnsuccessfully, false, true, true)]
        [TestCase((int)EngagementJobStatusEnum.AccountAutomationReconnectionNeeded, false, true, false)]
        [TestCase((int)EngagementJobStatusEnum.RanSuccessfully, false, true, true)]
        [TestCase((int)EngagementJobStatusEnum.Unknown, true, true, false)]
        [TestCase((int)EngagementJobStatusEnum.Unran, true, true, true)]
        [TestCase((int)EngagementJobStatusEnum.RanUnsuccessfully, true, true, true)]
        [TestCase((int)EngagementJobStatusEnum.InProgress, true, true, true)]
        [TestCase((int)EngagementJobStatusEnum.Expired, true, true, false)]
        [TestCase((int)EngagementJobStatusEnum.RetriedUnsuccessfully, true, true, true)]
        [TestCase((int)EngagementJobStatusEnum.AccountAutomationReconnectionNeeded, true, true, false)]
        [TestCase((int)EngagementJobStatusEnum.RanSuccessfully, true, true, true)]
        [Test]
        public void IsValidEngagmentJobStatusTest(int? status, bool allowNull, bool allowInProgress, bool expectedResult)
        {
            var result = CommonUtilities.IsValidEngagmentJobStatus(status, allowNull, allowInProgress);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
