
namespace MachineLearningIntelligenceAPI.DomainModels
{
    public class AnalyzeRequest
    {
        // The input strings for the analysis. Required.
        public List<string> InputStrings { get; set; }

        // The culture code that the AI will translate to.
        public string Culture { get; set; }

        // The prompt to explain the context of the input strings. Additional context to what the endpoint supports. Should only be system generated and not user input
        public string Prompt { get; set; }

        #region Equality Methods

        public override bool Equals(object obj)
        {
            var dto = obj as AnalyzeRequest;
            return dto != null && Equals(dto);
        }

        public bool Equals(AnalyzeRequest dto)
        {
            if (InputStrings != dto.InputStrings || Culture.SequenceEqual(dto.Culture) || Prompt.SequenceEqual(dto.Prompt))
                return false;

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ (InputStrings != null ? InputStrings.GetHashCode() : 0) ^ (Culture != null ? Culture.GetHashCode() : 0 ^ (Prompt != null ? Prompt.GetHashCode() : 0));
            return hashCode;
        }

        #endregion Equality Methods
    }
}
