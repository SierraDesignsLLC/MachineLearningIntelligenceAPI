using MachineLearningIntelligenceAPI.Common.Enums;

namespace MachineLearningIntelligenceAPI.Common.Utils
{
    public class CommonUtilities
    {
        // Used for Linq database queries
        public static List<int> ValidAutomationStatuses { get; private set; } = new List<int> { (int)AccountAutomationStatusEnum.Active, (int)AccountAutomationStatusEnum.Unknown, (int)AccountAutomationStatusEnum.Unverified };
        // Used for Linq database queries
        public static List<int> ValidAutomationJobStatusesAllowRetry { get; private set; }
            = new List<int> { (int)AccountAutomationJobStatusEnum.RanUnsuccessfully, (int)AccountAutomationJobStatusEnum.Unran, (int)AccountAutomationJobStatusEnum.RetriedUnsuccessfully, (int)AccountAutomationJobStatusEnum.ScheduledSuccessfully };
        // Used for Linq database queries
        public static List<int> ValidAutomationJobStatusesNoRetry { get; private set; }
            = new List<int> { (int)AccountAutomationJobStatusEnum.Unran, (int)AccountAutomationJobStatusEnum.ScheduledSuccessfully };
        // Used for Linq database queries
        public static List<int> ValidEngagementJobStatusesAllowRetry { get; private set; }
            = new List<int> { (int)EngagementJobStatusEnum.RanUnsuccessfully, (int)EngagementJobStatusEnum.Unran, (int)EngagementJobStatusEnum.RetriedUnsuccessfully, (int)EngagementJobStatusEnum.RanSuccessfully };
        // Used for Linq database queries
        public static List<int> ValidEngagementJobStatusesNoRetry { get; private set; }
            = new List<int> { (int)EngagementJobStatusEnum.Unran, (int)EngagementJobStatusEnum.RanSuccessfully };

        private const string TemporaryIdString = "aaaaaaaa-aaaa-aaaa-aaaa";

        public static string GenerateTemporaryId()
        {
            var tempId = TemporaryIdString + Guid.NewGuid().ToString().Substring(23);
            return tempId;
        }

        public static bool IsTemporaryId(string id)
        {
            if (id?.Length >= 23 && id.Substring(0, 23) == TemporaryIdString)
            {
                return true;
            }
            return false;
        }

        public static bool IsValidAccountAutomationStatus(int automationStatus)
        {
            if (ValidAutomationStatuses.Contains(automationStatus))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidAccountAutomationJobStatus(int? status, bool allowNullJobStatus = false, bool allowInProgress = false)
        {
            if (status == null && allowNullJobStatus == true
                || status == (int)AccountAutomationJobStatusEnum.InProgress && allowInProgress == true
                || ValidAutomationJobStatusesAllowRetry.Contains(status ?? int.MinValue))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidEngagmentJobStatus(int? status, bool allowNullJobStatus = false, bool allowInProgress = false, bool allowExpired = false)
        {
            if (status == null && allowNullJobStatus == true
                || status == (int)EngagementJobStatusEnum.InProgress && allowInProgress == true
                || status == (int)EngagementJobStatusEnum.Expired && allowExpired == true
                || ValidEngagementJobStatusesAllowRetry.Contains(status ?? int.MinValue))
            {
                return true;
            }
            return false;
        }
    }
}
