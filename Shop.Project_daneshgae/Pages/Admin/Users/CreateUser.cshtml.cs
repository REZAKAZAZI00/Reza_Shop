


namespace Shop.Project_daneshgae.Pages.Admin.Users
{
    [PermissionChecker(3)]
    public class CreateUserModel : PageModel
    {
        private readonly IUserServics _userServics;
        private readonly IPermissionServics _permissionServics;

        public CreateUserModel(IPermissionServics permissionServics,IUserServics userServics)
        {
            _permissionServics=permissionServics;
            _userServics=userServics;
        }
        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permissionServics.GetRoles();

        }

        public IActionResult OnPost(List<int> SelectedRoles) 
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            int userId = _userServics.AddUserFromAdmin(CreateUserViewModel);
            _permissionServics.AddRolesToUser(SelectedRoles,userId);
            return Redirect("Admin/Users");
        }
    }
}
