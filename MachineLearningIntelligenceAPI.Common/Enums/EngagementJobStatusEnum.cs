namespace MachineLearningIntelligenceAPI.Common.Enums
{
    public enum EngagementJobStatusEnum
    {
        Unknown = -3, // Result of unhandled exception throwing along the job processing code path in which it cannot be automatically determined if the job ran succesfully
        Expired = -2, // Job is expired and cannot be reran, a way to disable the job
        RanSuccessfully = -1, // Job ran successfully, and is ready to be reran
        Unran = 1, // Unran engagment job status, no known issues. When reenabled, this is the inital status as well
        InProgress = 2, // engagment job status, currently running and is in progress
        RanUnsuccessfully = 3, // Job ran unsuccessfully and has an error
        RetriedUnsuccessfully = 4, // Job retried unsuccessfully and has an error, this is an automatic retry 1 hour after trigger on time. 
        AccountAutomationReconnectionNeeded = 5, // 403 or 401 issues with login, potential password change. Ran unsuccessfully and has an error. Cooldown before next retry, or requires account to be reconnected
    }
}
