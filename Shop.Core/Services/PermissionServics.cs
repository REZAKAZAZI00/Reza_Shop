using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Services
{
    public class PermissionServics : IPermissionServics
    {
        private readonly ShopDbContext _context;
        public PermissionServics(ShopDbContext context)
        {
            _context=context;
        }

        public void AddRolesToUser(List<int> roleIds, int userId)
        {
            foreach (var orleid in roleIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = orleid,
                    UserId = userId
                });
            }
            _context.SaveChanges();
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
        public void EditRolesUser(int userId, List<int> rolesId)
        {
            //Delete All Roles User
            _context.UserRoles.Where(r => r.UserId == userId).ToList().ForEach(r => _context.UserRoles.Remove(r));

            //Add New Roles
            AddRolesToUser(rolesId, userId);
        }

        public int AddRoles(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public void UpdateRoles(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public void DeleteRoles(Role role)
        {
            role.IsDelete = true;
            UpdateRoles(role);
        }

        public List<Permission> GetAllPermission()
        {
            return _context.Permission.ToList();
        }

        public void AddPermissionToRole(int roleId, List<int> permission)
        {
            foreach (var p in permission)
            {
                _context.RolePermission.Add(new RolePermission() {
                    PermissionId = p,
                    RoleId =roleId,
                });
            }
            _context.SaveChanges();
        }

        public List<int> PermissionsRoles(int roleId)
        {
            return _context.RolePermission.
                Where(p => p.RoleId == roleId)
                .Select(p => p.PermissionId).ToList();
        }

        public void UpdatePermissionsRole(int roleId, List<int> permission)
        {

            _context.RolePermission.Where(p => p.RoleId == roleId).ToList()
                .ForEach(p => _context.RolePermission.Remove(p));

            AddPermissionToRole(roleId, permission);
        }

        public bool CheckPermission(int permissionId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            List<int> userRoles = _context.UserRoles.Where(u => u.UserId == userId)
                .Select(r => r.RoleId).ToList();
            if (!userRoles.Any())
                return false;

            List<int> RolesPermission = _context.RolePermission.
                Where(r => r.PermissionId == permissionId).Select(p => p.RoleId).ToList();
            return RolesPermission.Any(p=> userRoles.Contains(p));
        }
    }
}
