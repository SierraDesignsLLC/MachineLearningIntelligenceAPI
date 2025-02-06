namespace MachineLearningIntelligenceAPI.Common.Enums
{
    // TODO: this should be static and be retrieved from the database. If we ever need to update the database it may be easier to pull types everytime and not make them static because it would require restarting the server instance to pull them again.
    // could just keep them here and then coordinate a database update with the code release. Would have to make this not static and pull types everytime, then update the database records and it will be pulling what is the accurate enum. Data integrity at 
    // the cost of db requests.
    public enum AccountAutomationStatusEnum
    {
        Unknown = -1, // Transitional automation status, the requested platform could be unavailable for an indeterminate reason
        Active = 0, // Working automation status, no known issues
        Unverified = 1, // Status requiring automation to be verified with registering platform
        Prohibited = 2, // Status representing the automation has been banned
        Unauthenticated = 3, // Status representing the authentication is not working, password change or user removed oauth connection
    }
}
