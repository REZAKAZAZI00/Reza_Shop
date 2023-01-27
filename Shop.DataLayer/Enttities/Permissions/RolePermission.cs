

namespace Shop.DataLayer.Enttities.Permissions
{
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }
        public int RoleId { get; set; }

        public int PermissionId { get; set; }


        #region Relations
        public virtual Permission Permission { get; set; }
        public virtual  Role Role { get; set; }
        #endregion

    }
}
