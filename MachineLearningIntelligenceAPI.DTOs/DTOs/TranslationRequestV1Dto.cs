
namespace MachineLearningIntelligenceAPI.DTOs
{
    public class TranslationRequestV1Dto : DtoBase<TranslationRequestV1Dto>, IDtoBase
    {
        // The input strings for the translation. Required.
        public List<string> InputStrings { get; set; }

        // The language that the AI will translate to. Required.
        public string Culture { get; set; }

        #region Equality Methods

        public override bool Equals(object obj)
        {
            var dto = obj as TranslationRequestV1Dto;
            return dto != null && Equals(dto);
        }

        public bool Equals(TranslationRequestV1Dto dto)
        {
            if (!base.Equals(dto))
                return false;

            if (InputStrings != dto.InputStrings || Culture.SequenceEqual(dto.Culture))
                return false;

            return true;
        }

        /// <summary>
        /// Override of hash code
        /// </summary>
        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = hashCode * 397 ^ (InputStrings != null ? InputStrings.GetHashCode() : 0) ^ (Culture != null ? Culture.GetHashCode() : 0);
            return hashCode;
        }

        #endregion Equality Methods
    }
}
