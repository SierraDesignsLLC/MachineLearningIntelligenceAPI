namespace MachineLearningIntelligenceAPI.DomainModels
{
    public interface IModelBase : IModelBaseNoId
    {
        /// <summary>
        /// The primary key of this object
        /// </summary>
        Guid? Id { get; set; }
    }

    public interface IModelBaseNoId
    {
        /// <summary>
        /// The timestamp of when this object was created
        /// </summary>
        DateTime? Created { get; set; }

        /// <summary>
        /// The timestamp of when this object was last modified
        /// </summary>
        DateTime? Modified { get; set; }

        /// <summary>
        /// The User Account ID of the contact who created this object
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// The User Account ID of the contact who last modified this object
        /// </summary>
        string ModifiedBy { get; set; }
    }
}
