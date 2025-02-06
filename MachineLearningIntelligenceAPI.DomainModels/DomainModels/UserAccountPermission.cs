namespace MachineLearningIntelligenceAPI.DomainModels
{
    public class UserAccountPermission : ModelBase<UserAccountPermission>
    {
        public string UserAccountId { get; set; }
        public string PermissionName { get; set; }
        public int PermissionTypeId { get; set; } // iac frp role int type retrieved from database when set

        #region Equality Methods

        public override bool Equals(object userAccountPermissionObject)
        {
            var userAccountPermission = userAccountPermissionObject as UserAccountPermission;
            return userAccountPermission != null && Equals(userAccountPermission);
        }

        public override bool Equals(UserAccountPermission userAccountPermission)
        {
            if (!base.Equals(userAccountPermission))
                return false;

            if (UserAccountId != userAccountPermission.UserAccountId || PermissionName != userAccountPermission.PermissionName || PermissionTypeId != userAccountPermission.PermissionTypeId)
                return false;

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
                hashCode = hashCode * 397 ^ UserAccountId.GetHashCode() ^ PermissionName.GetHashCode() ^ PermissionTypeId.GetHashCode();
                return hashCode;
            }
        }

        #endregion Equality Methods
    }
}
