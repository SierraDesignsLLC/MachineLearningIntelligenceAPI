namespace MachineLearningIntelligenceAPI.DataAccess.Repositories.Interfaces
{
    public interface IMachineLearningIntelligenceAPIRepository
    {
        /// <summary>
        /// Gets and runs scheduled jobs, designed for distributed computing where multiple servers should avoid accessing the same entities. 
        /// Run from the background service to an http call to properly setup dependency injection and not run from the background service
        /// </summary>
        public void GetAndRunScheduledAccountAutomationJobs(bool retry = false);

        /// <summary>
        /// Gets and runs scheduled jobs, designed for distributed computing where multiple servers should avoid accessing the same entities. Need to break out scheduler service to its own service
        /// Run from the background service to an http call to properly setup dependency injection and not run from the background service
        /// </summary>
        public void GetAndRunCommunityEngagementJobs();

        /// <summary>
        /// Create account automation data for provided IDs
        /// </summary>
        public void CreateAccountAutomationData(Guid userAccountId, List<Guid?> accountAutomationIdsToRun);

        /// <summary>
        /// Run create account automation data
        /// </summary>
        public void RunAccountAutomationDataJob();
    }
}
