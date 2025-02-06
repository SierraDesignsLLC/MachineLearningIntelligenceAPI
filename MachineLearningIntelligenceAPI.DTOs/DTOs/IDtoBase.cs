namespace MachineLearningIntelligenceAPI.DTOs
{
    public interface IDtoBase
    {

        /// <summary>
        /// The time when this item was last changed in the database
        /// </summary>
        DateTime? Modified { get; set; }

        /// <summary>
        /// The time when this item was created in the database
        /// </summary>
        DateTime? Created { get; set; }

        /// <summary>
        /// The User Account Id of the creator of this item
        /// </summary>
        string? CreatedBy { get; set; }

        /// <summary>
        /// The User Account Id of the modifier of this item
        /// </summary>
        string? ModifiedBy { get; set; }
    }
}
