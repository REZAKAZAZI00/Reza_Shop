

namespace Shop.Project_daneshgae.Pages.Admin.Users
{
    [PermissionChecker(5)]
    public class DeleteUserModel : PageModel
    {
        private readonly IUserServics _userServics;
        public DeleteUserModel(IUserServics  userServics)
        {
            _userServics = userServics; 
        }

         
        public InformationUserViewModel user { get; set; }
        
        public void OnGet(int id)
        {
            ViewData["UserId"] = id;
            user =_userServics.GetUserInformation(id);
        }
        public IActionResult OnPost(int UserId)
        {
           
            _userServics.DeleteUser(UserId);
            return Redirect("/admin/users");
        }
    }
}
