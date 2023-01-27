
namespace Shop.DataLayer.Enttities.User
{
    public class UserRole
    {
        public UserRole()
        {
            
        }
        [Key]
        public int UR_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Relations

        public virtual Role Roles { get; set; }
        public virtual User  Users { get; set; }

        #endregion

    }
}
