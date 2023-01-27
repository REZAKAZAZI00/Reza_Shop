using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Project_daneshgae.Pages.Admin.Roles
{
    [PermissionChecker(9)]
    public class EditRoleModel : PageModel
    {
        private readonly IPermissionServics _permissionServics;
        public EditRoleModel(IPermissionServics permissionServics)
        {
                _permissionServics=permissionServics;
        }
        [BindProperty]
        public Role role { get; set; }
        public void OnGet(int id)
        {
            ViewData["Permissin"] = _permissionServics.GetAllPermission();

            ViewData["SelectedPermissins"] = _permissionServics.PermissionsRoles(id);
            role =_permissionServics.GetRoleById(id);    

        }

        public IActionResult OnPost(List<int> SelectedPermission) 
        {
            

            _permissionServics.UpdateRoles(role);

            _permissionServics.UpdatePermissionsRole(role.RoleId, SelectedPermission);
            return Redirect("/admin/Roles");
        
        }

    }
}
