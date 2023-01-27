
namespace Shop.Project_daneshgae.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Route("Profile")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserServics _userService;

        public HomeController(IUserServics userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(_userService.GetUserInformation(User.Identity.Name));
        }
        #region EditProfile
        [Route("EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetDataForEditProfileUserPanel(User.Identity.Name));  
        }

        [Route("EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditProfileUserPanelViewModel editProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(editProfile);
            }

          var result=  _userService.UpdateProfile(User.Identity.Name, editProfile);
            if (result == 1)
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Redirect("/Login?EditProfile=true");
            }
            else
            {
                return Redirect("/Profile");
            }
        }
        #endregion


        #region ChangePassWord

        [Route("ChangePassWord")]
        public IActionResult ChangePassWord()
        {
            return View();
        }
        [Route("ChangePassWord")]
        [HttpPost]
        public IActionResult ChangePassWord(ChangePassWordViewModel change)
        {
            string CurrentUser = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return View(change);
            }

            if (!_userService.CompareOldPassWord(CurrentUser,change.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صیحیح نمی باشد");
                return View(change);
            }
            _userService.ChangeUserPasswword(CurrentUser, change.Password);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion
    }
}