

namespace Shop.Project_daneshgae.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserServics _userServics;
        private readonly IProductServices _productServices;
        public HomeController(IUserServics userServics,IProductServices productServices)
        {
                _userServics = userServics;
            _productServices = productServices;
        }
        [Route("")]
        [Route("index")]
        [Route("home/index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Content")]
        [Route("home/Content")]
        [Authorize]
        public IActionResult Content()
        {
            return View();
        }
       
        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            var wallet=  _userServics.GetWalletByWalletId(id);
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

               

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userServics.UpdateWallet(wallet);
                }

            }

            return View();
        }


        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> Lists = new () 
            { 
                 new SelectListItem() {Text=" انتخاب کنید",Value="" }
            
            };
            Lists.AddRange(_productServices.GetSubGroupForManageProduct(id));
            return Json(new SelectList(Lists,"Value","Text"));
        }

    }
}
