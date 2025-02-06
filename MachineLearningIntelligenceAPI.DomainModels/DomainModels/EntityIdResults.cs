namespace MachineLearningIntelligenceAPI.DomainModels
{
    /// <summary>
    /// Generic Dto class that contains a list of succeeded and failed ids 
    /// </summary>
    public class EntityIdResults
    {
        public List<string> SucceededIds { get; set; } = new List<string>();
        public List<string> FailedIds { get; set; } = new List<string>();

        #region Equality Methods
        public override bool Equals(object results)
        {
            var entityIdResults = results as EntityIdResults;
            return entityIdResults != null && Equals(entityIdResults);
        }

        public bool Equals(EntityIdResults entityIdResults)
        {
            if (entityIdResults.SucceededIds.Count != SucceededIds.Count || entityIdResults.FailedIds.Count != FailedIds.Count)
            {
                return false;
            }

            // order does matter for this equality check even though there is no sequence number
            var idx = 0;
            foreach (var id in entityIdResults.SucceededIds)
            {
                if (id != SucceededIds.ElementAt(idx))
                {
                    return false;
                }
                idx++;
            }

            idx = 0;
            foreach (var id in entityIdResults.FailedIds)
            {
                if (id != FailedIds.ElementAt(idx))
                {
                    return false;
                }
                idx++;
            }

            return true;
        }

        /// <summary>
        /// Check the hash code to see if we should bother checking Equals
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = hashCode * 397 ^ SucceededIds.GetHashCode() ^ FailedIds.GetHashCode();
                return hashCode;
            }
        }

        #endregion Equality Methods
    }
}
