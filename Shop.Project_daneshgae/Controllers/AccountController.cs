
namespace Shop.Project_daneshgae.Controllers
{
    public class AccountController : Controller
    {

        readonly private IUserServics _userServics;
        readonly private IViewRenderService _viewRenderService;
        public AccountController(IUserServics userServics, IViewRenderService viewRenderService)
        {
            _userServics = userServics;
            _viewRenderService = viewRenderService;
            
        }
        #region Register
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegiserViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (_userServics.IsExistEmail(FixedText.FexedEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");

                return View(register);
            }
            if (_userServics.IsExistUsername(register.Password))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");

                return View(register);
            }
            User user = new()
            {
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Email = FixedText.FexedEmail(register.Email),
                IsActive = false,
                CodeMelli = "0",
                Password = PassWordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "Defult.jpg",
                Lname = "",
                Name = "",
                UserName = register.UserName,
                PostalCode = "0",
                PhoneNamber = "0",
                
            };
            var body = _viewRenderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email,"فعال سازی ",body);

            _userServics.AddUser(user);
            return View("SuccssesRegister",user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IsExistEmail(string email)
        {
            var user =  _userServics.IsExistEmail(email);
            if (user == false) { return Json(true); }
            return Json("ایمیل وارد شده از قبل موجود است");
        }
        
        
        public IActionResult IsExistUsername(string UserName)
        {
            var user = _userServics.IsExistUsername(UserName);
            if (user == false) { return Json(true); }
            return Json("نام کاربری وارد شده از قبل موجود است");
        }
        #endregion

        #region Login
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userServics.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName),
                        

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    { 
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");

            return View();
        }


        #endregion

        #region  ActiveAccount

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userServics.ActiveAccount(id);
            return View();
        }
        #endregion

        #region ForgetPassWord
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmailIsExist(string email)
        {
            var user = _userServics.IsExistEmail(email);
            if (user == true) { return Json(true); }
            return Json("ایمیل وارد شده از قبل موجود نیست");
        }
        [Route("ForgotPassWord")]
        public IActionResult ForgetPassWord()
        {
            return View();
        }
        [Route("ForgotPassWord")][HttpPost]
        public IActionResult ForgetPassWord(ForgotPassWordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }
            string fixedEmail = FixedText.FexedEmail(forgot.Email);
            var email = _userServics.IsExistEmail(forgot.Email);
            if (email == false) 
            {  
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                return View(forgot);
            }
            var user=_userServics.GetUserByEmail(fixedEmail);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری یافت نشد");
                return View(forgot);
            }
            var body = _viewRenderService.RenderToStringAsync("_ForgotPassWord", user);
            SendEmail.Send(user.Email, " بازیابی کلمه عبور ", body);
            return View();
        }
        #endregion

        #region ResetPassword
        [HttpGet]
        [Route("ResetPassword/{id}")]
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            }); 
        }
        [HttpPost]
        [Route("ResetPassword/{id}")]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
            {
                return View(reset);
            }
            var user = _userServics.GetUserByActiveCode(reset.ActiveCode);
            if (user == null)
                return NotFound();
            string hashNewPassword = PassWordHelper.EncodePasswordMd5(reset.Password);
            user.Password = hashNewPassword;
            _userServics.UpdateUser(user);
            return Redirect("/Login");
        }
        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {  
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion


    }
}

