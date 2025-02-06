using MachineLearningIntelligenceAPI.DomainModels;
using System.Globalization;

namespace MachineLearningIntelligenceAPI.DataAccess
{
    // TODO TEST that each request creates a new object but static for the lifetime of the call? maybe security issue. but just the call not the whole server instance running
    public class RequestSessionInformation
    {
        /// <summary>
        /// Authentication token from the session token passed in on the request in the auth header, null if system.
        /// </summary>
        public Guid? Authentication { get; set; } = null;
        /// <summary>
        /// User Account Id, System Id, or Service Account Id. The caller's id uses for database auditing purposes. Created by, modified by, etc.
        /// </summary>
        public Guid? RequestUserId { get; set; } = null;
        /// <summary>
        /// Flag that is set when the passed in authentication is validated
        /// </summary>
        public bool HasValidatedAuthentication { get; set; } = false;

        public List<UserAccountPermission> Permissions { get; set; }

        /// <summary>
        /// The timezone offset in minutes of a request, if available, can be null.
        /// Set by end users, machine to machine calls can be null if not acting for a user
        /// Set by header X-Timezone-Offset
        /// For UX purposes mostly
        /// </summary>
        public int? TimezoneOffset { get; set; } // in minutes

        /// <summary>
        /// Set by end users, machine to machine calls can be null if not acting for a user
        /// </summary>
        public CultureInfo? Locale { get; set; }

    }
}
