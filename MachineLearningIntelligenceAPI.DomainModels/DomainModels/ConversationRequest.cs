
namespace MachineLearningIntelligenceAPI.DomainModels
{
    public class ConversationRequest
    {
        // The input string for the conversation. Required.
        public string InputString { get; set; }

        // The role that the AI will assume and refer to itself as
        public string Role { get; set; } = "assistant";

        /// <summary>
        /// Context format:
        ///     Brand/Company information, mission statement context: General information about the company/ brand, potentially including mission statement.
        ///     Availability and hours of operation context: Information about company availability or particular service's availability.
        ///     Product information context: Information about featured products available, or all products in general.
        ///     Writing style context: How the response should be written in terms of vocabulary and diction.
        ///     Response style context: How the response should be written in terms of structure, demeanor, tone, and feel.
        ///     Technical support context: More advanced technical support information to guide user's with their issues.
        ///     Guardrails: Make sure the responses are appropriate and do not violate the platforms TOS. this is always first and last to ensure quality... TODO: test that this is right...
        ///                 Cannot indicate it is a bot
        /// </summary>
        // Any context strings to provide to the AI model to give more context. Explicitly seperate messages, 1 message per element
        public List<string> Context { get; set; }

        #region Equality Methods
        public override bool Equals(object obj)
        {
            var model = obj as ConversationRequest;
            return model != null && Equals(model);
        }

        public bool Equals(ConversationRequest model)
        {
            if (InputString != model.InputString || Role != model.Role || Context != model.Context)
                return false;

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ InputString.GetHashCode() ^ Role.GetHashCode() ^ Context.GetHashCode();
            return hashCode;
        }
        #endregion Equality Methods
    }
}
