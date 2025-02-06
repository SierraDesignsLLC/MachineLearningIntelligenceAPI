using System.Runtime.Serialization;

namespace MachineLearningIntelligenceAPI.DTOs
{
    /// <summary>
	/// Base class to inherit from for entity properites
	/// </summary>
	[DataContract(Namespace = "http://sierradesignsllc.com/api/MachineLearningIntelligenceAPI/v1")]
    public abstract class DtoBase<TModel> : IEquatable<TModel> where TModel : IDtoBase
    {

        /// <summary>
        /// The time when this item was last changed in the database. This value should only be set by the server. When updating an object an
        /// exception stating that the object is out of synch will occur if the changed time does not match the one that exists in the database.
        /// If that happens it usually means that another user has modified the value after this object was pulled down.
        /// </summary>
        public virtual DateTime? Modified { get; set; }

        /// <summary>
        /// The time when this item was created in the database. This value should only be set by the server and should never change.
        /// </summary>
        public virtual DateTime? Created { get; set; }

        /// <summary>
        /// The User Account Id of the creator of this item. This value should only be set by the server and should never change.
        /// </summary>
        public virtual string? CreatedBy { get; set; }

        /// <summary>
        /// The User Account Id of the most recent modifier of this item. This value should only be set by the server.
        /// </summary>
        public virtual string? ModifiedBy { get; set; }

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
        /// Uses <see cref="object.MemberwiseClone"/> to shallow copy the account and then replaces the referenced objects with subsequent 
        /// shallow copies. Lists are created as new lists and shallow copies of each object in the list are created.
        /// </summary>
        /// <returns>A TModel that has been shallow copied with the EntityDtoBase portion deep copied</returns>
        public virtual TModel DeepCopy()
        {
            // All properties are value types or strings so deep copy and shallow copy are the same
            var entityDto = ShallowCopy();

            return entityDto;
        }

        #endregion Copy Methods

        #region Operator Overload
        /// <summary>
        /// Operator overload of == to detemine if the entityDtos are equal
        /// </summary>
        public static bool operator ==(DtoBase<TModel> entityDtoHasOwnerA, DtoBase<TModel> entityDtoHasOwnerB)
        {
            // Not checking the reference because we are not overloading GetHashCode() properly

            // Cast to object to prevent an infinite loop
            if ((object)entityDtoHasOwnerA == null ^ (object)entityDtoHasOwnerB == null)
                return false;

            // If entityDtoA is null, then we know entityDtoB is null, otherwise check the values for equality
            return (object)entityDtoHasOwnerA == null || entityDtoHasOwnerA.Equals(entityDtoHasOwnerB);
        }

        /// <summary>
        /// Operator overload of != to detemine if the entityDtos are not equal
        /// </summary>
        public static bool operator !=(DtoBase<TModel> entityDtoHasOwnerA, DtoBase<TModel> entityDtoHasOwnerB)
        {
            return !(entityDtoHasOwnerA == entityDtoHasOwnerB);
        }

        #endregion Operator Overload

        #region Equality Methods

        /// <summary>
        /// Compares the properties of this base class. Objects that inherit from this and override Equals() should call base.Equals().
        /// </summary>
        /// <param name="obj">The object to compare to this object</param>
        /// <returns>Whether or not this object and the passed in object are equal</returns>
        public abstract override bool Equals(object obj);

        /// <summary>
        /// Compare two objects that inherit from this type
        /// </summary>
        public virtual bool Equals(TModel entityDto)
        {
            // An existing object cannot be equal to null
            if (entityDto == null)
                return false;

            if (!Equals(Modified, entityDto.Modified) || !Equals(Created, entityDto.Created) || CreatedBy != entityDto.CreatedBy ||
                ModifiedBy != entityDto.ModifiedBy)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// GetHashCode function override to generate a hash code for EntityDtoBase
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Created.GetHashCode() ^ Modified.GetHashCode() ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0) ^ (ModifiedBy != null ? ModifiedBy.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion Equality Methods

    }
}
