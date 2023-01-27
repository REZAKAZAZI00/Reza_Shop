using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Project_daneshgae.Pages.Admin.Users
{
    [PermissionChecker(6)]
    public class ListDeleteUserModel : PageModel
    {
       private readonly IUserServics _userServics;
        public ListDeleteUserModel(IUserServics userServics)
        {
            _userServics = userServics;
        }
        public UserForAdminViewModel user { get; set; }
        public void OnGet()
        {
            user = _userServics.GetUsersForListDeleteUsers();
        }
    }
}
