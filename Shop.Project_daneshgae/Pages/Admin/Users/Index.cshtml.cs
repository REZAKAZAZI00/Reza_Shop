using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Project_daneshgae.Pages.Admin.Users
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private readonly IUserServics _userServics;
        public IndexModel(IUserServics userServics)
        {
            _userServics  = userServics;
        }
        public UserForAdminViewModel   UserForAdminViewModel { get; set; }
        public void OnGet(int pageid = 1, string filtercodeMelli = "", string filterEmail = "", string filterUserName = "")
        {
            UserForAdminViewModel = _userServics.GetUsers(pageid,filtercodeMelli,filterEmail,filterUserName);
        }
    }
}
