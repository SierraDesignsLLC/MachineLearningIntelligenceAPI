
namespace MachineLearningIntelligenceAPI.DomainModels
{
    public class TranslationRequest
    {
        // The input strings for the translation. Required.
        public List<string> InputStrings { get; set; }

        // The language that the AI will translate to. Required.
        public string Culture { get; set; }

        #region Equality Methods
        public override bool Equals(object obj)
        {
            var model = obj as TranslationRequest;
            return model != null && Equals(model);
        }

        public bool Equals(TranslationRequest model)
        {
            if (InputStrings != model.InputStrings || Culture != model.Culture)
                return false;

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ InputStrings.GetHashCode() ^ Culture.GetHashCode();
            return hashCode;
        }
        #endregion Equality Methods
    }
}
