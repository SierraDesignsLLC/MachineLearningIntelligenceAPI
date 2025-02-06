namespace MachineLearningIntelligenceAPI.DomainModels
{
    /// <summary>
	/// Entity Model for common model properties
	/// </summary>
	[Serializable]
    public abstract class ModelBase<TModel> : IModelBase, IEquatable<TModel> where TModel : IModelBase
    {

        /// <summary>
        /// The primary key of this object
        /// </summary>
        public virtual Guid? Id { get; set; }

        /// <summary>
        /// The timestamp of when this object was created
        /// </summary>
        public virtual DateTime? Created { get; set; }

        /// <summary>
        /// The timestamp of when this object was last modified
        /// </summary>
        public virtual DateTime? Modified { get; set; }

        /// <summary>
        /// The User Account ID of the contact who created this object
        /// </summary>
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// The User Account ID of the contact who last modified this object
        /// </summary>
        public virtual string ModifiedBy { get; set; }

        #region Copy Methods

        /// <summary>
        /// Uses <see cref="object.MemberwiseClone"/> to shallow copy the TModel. This works the same at any depth so there is no reason to override.
        /// </summary>
        /// <returns>A shallow copied TModel</returns>
        public virtual TModel ShallowCopy()
        {
            return (TModel)MemberwiseClone();
        }

        /// <summary>
        /// Uses <see cref="object.MemberwiseClone"/> to shallow copy the model and then replaces the referenced objects with subsequent 
        /// shallow copies. Lists are created as new lists and shallow copies of each object in the list are created.
        /// </summary>
        /// <returns>A TModel that has been shallow copied with the ModelBase portion deep copied</returns>
        public virtual TModel DeepCopy()
        {
            // All properties are value types or strings so deep copy and shallow copy are the same
            var ModelBase = ShallowCopy();

            return ModelBase;
        }

        #endregion Copy Methods

        #region Operator Overload

        /// <summary>
        /// Override the equal to opperator for this type
        /// </summary>
        public static bool operator ==(ModelBase<TModel> ModelBaseA, ModelBase<TModel> ModelBaseB)
        {
            // Not checking the reference because we are not overloading GetHashCode() properly

            // Cast to object to prevent an infinite loop
            if ((object)ModelBaseA == null ^ (object)ModelBaseB == null)
                return false;

            // If ModelBaseA is null, then we know ModelBaseB is null, otherwise check the values for equality
            return (object)ModelBaseA == null || ModelBaseA.Equals(ModelBaseB);
        }

        /// <summary>
        /// Override the not equals opperator for this type
        /// </summary>
        public static bool operator !=(ModelBase<TModel> ModelBaseA, ModelBase<TModel> ModelBaseB)
        {
            return !(ModelBaseA == ModelBaseB);
        }

        #endregion Operator Overload

        #region Equality Methods

        /// <summary>
        /// Force objects that inherit form this type to override the comparison to an unkown object type
        /// </summary>
        public abstract override bool Equals(object ModelBase);

        /// <summary>
        /// Compare two objects that inherit from this type
        /// </summary>
        public virtual bool Equals(TModel ModelBase)
        {
            // An existing object cannot be equal to null
            if (ModelBase == null)
                return false;

            if (Id != ModelBase.Id || Created != ModelBase.Created || Modified != ModelBase.Modified)
                return false;

            if (ModifiedBy != ModelBase.ModifiedBy || CreatedBy != ModelBase.CreatedBy)
                return false;

            return true;
        }

        /// <summary>
        /// A simple XOR hash code. Things like Enumerable.Except() will check the hash and then call Equals() after
        /// finding a match. This should prevent the need to call Equals() every time if there are values present
        /// and they don't match.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id != null ? Id.GetHashCode() : 0;
                hashCode = hashCode * 397 ^ Created.GetHashCode();
                hashCode = hashCode * 397 ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion Equality Methods
    }
}
