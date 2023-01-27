using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Project_daneshgae.Pages.Admin.Users
{
    [PermissionChecker(4)]
    public class EditUserModel : PageModel
    {
        private readonly IUserServics _userServics;
        private readonly IPermissionServics _permissionServics;
        public EditUserModel(IUserServics userServics,IPermissionServics permissionServics)
        {
            _userServics = userServics;
            _permissionServics = permissionServics;
        }
        [BindProperty]
        public EditUserViewModel user { get; set; }
        public void OnGet(int id)
        {
            ViewData["Roles"] = _permissionServics.GetRoles();

           user = _userServics.GetUserForShowInEditMode(id);
        }


        public IActionResult OnPost(List<int> SelectedRoles)
        {
          
            _userServics.EditUserFromAdmin(user);
            _permissionServics.EditRolesUser(user.UserId,SelectedRoles);

            return Redirect("/admin/users");
        }
    }
}
