

namespace Shop.Project_daneshgae.Pages.Admin.Roles
{
    [PermissionChecker(8)]
    public class CreateRoleModel : PageModel
    {
        private readonly IPermissionServics _permissionServics;
        public CreateRoleModel(IPermissionServics permissionServics)
        {
            _permissionServics=permissionServics;
        }
        [BindProperty]
        public Role roles { get; set; }
        public void OnGet()
        {
            ViewData["Permissin"] = _permissionServics.GetAllPermission();
        }

        public IActionResult OnPost(List<int> SelectedPermission) 
        {
            roles.IsDelete = false;
            int rolesId=_permissionServics.AddRoles(roles);
            _permissionServics.AddPermissionToRole(rolesId,SelectedPermission);
            return Redirect("/Admin/Roles");
        }
    }
}
