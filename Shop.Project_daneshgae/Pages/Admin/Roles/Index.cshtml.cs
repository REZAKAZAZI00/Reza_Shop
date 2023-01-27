

namespace Shop.Project_daneshgae.Pages.Admin.Roles
{
    [PermissionChecker(7)]
    public class IndexModel : PageModel
    {
        private readonly IPermissionServics _permissionServics;
        public IndexModel(IPermissionServics permissionServics)
        {
            _permissionServics=permissionServics;
        }
        public List<Role>  RolesList { get; set; }
        public void OnGet()
        {
            RolesList=_permissionServics.GetRoles();
        }
    }
}
