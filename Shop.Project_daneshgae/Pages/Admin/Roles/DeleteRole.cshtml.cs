using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Project_daneshgae.Pages.Admin.Roles
{
    [PermissionChecker(10)]
    public class DeleteRoleModel : PageModel
    {
        private readonly IPermissionServics _permissionServics; 
        public DeleteRoleModel(IPermissionServics    permissionServics)
        {
            _permissionServics = permissionServics;
        }
        [BindProperty]
        public   Role  Role  { get; set; }
        public void OnGet(int id)
        {
            Role=_permissionServics.GetRoleById(id);
        }
        public IActionResult OnPost() 
        {
          
            _permissionServics.DeleteRoles(Role);
            return Redirect("/admin/Roles");
        }
    }
}
