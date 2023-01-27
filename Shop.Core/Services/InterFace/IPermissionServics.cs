using System;


namespace Shop.Core.Services.InterFace
{
    public interface IPermissionServics
    {
        #region Roles

        List<Role> GetRoles();
        int AddRoles(Role role);
        void UpdateRoles(Role role);
        void DeleteRoles(Role role);    
        Role GetRoleById(int roleId);
        void AddRolesToUser(List<int> roleIds,int userId);
        void EditRolesUser(int userId, List<int> rolesId);
        #endregion
        #region Permissions
        List<Permission> GetAllPermission();
        List<int> PermissionsRoles(int roleId);
        void AddPermissionToRole(int roleId, List<int> permission);
        void UpdatePermissionsRole(int roleId, List<int> permission);
        bool CheckPermission(int permissionId,string userName);
        #endregion
    }
}
