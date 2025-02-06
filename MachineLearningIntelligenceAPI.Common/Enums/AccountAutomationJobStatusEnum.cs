namespace MachineLearningIntelligenceAPI.Common.Enums
{
    // TODO: this should be static and be retrieved from the database. If we ever need to update the database it may be easier to pull types everytime and not make them static because it would require restarting the server instance to pull them again.
    // could just keep them here and then coordinate a database update with the code release. Would have to make this not static and pull types everytime, then update the database records and it will be pulling what is the accurate enum. Data integrity at 
    // the cost of db requests.
    public enum AccountAutomationJobStatusEnum
    {
        Unknown = -3, // Result of unhandled exception throwing along the job processing code path in which it cannot be automatically determined if the job ran succesfully
        ScheduledSuccessfully = -2, // Job scheduled successfully and is ready to be ran on scheduled trigger time
        RanSuccessfully = -1, // Job ran successfully
        Unran = 1, // Unran automation job status, no known issues
        InProgress = 2, // Automation job status, currently running and is in progress
        RanUnsuccessfully = 3, // Job ran unsuccessfully and has an error
        RetriedUnsuccessfully = 4, // Job retried unsuccessfully and has an error, this is an automatic retry 1 hour after trigger on time. 
        ManuallyRetriedUnsuccessfully = 5, // Job manually retried, or 2nd retry 1 day after 1st retry, unsuccessfully and has an error. Cooldown before next retry, or requires account to be reconnected?
        FailedToPersistPayloadRemotely = 6, // Job unsuccessfully published its payload and now the media is lost because we do not persist the media content
    }
}
