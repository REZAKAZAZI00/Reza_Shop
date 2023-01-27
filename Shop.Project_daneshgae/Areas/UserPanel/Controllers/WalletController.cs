using Microsoft.AspNetCore.Mvc;

namespace Shop.Project_daneshgae.Areas.UserPanel.Controllers
{
    [Authorize]
    [Area("UserPanel")]
   
    public class WalletController : Controller
    {
       private readonly IUserServics _userServics;
        public WalletController(IUserServics userServics)
        {
            _userServics = userServics;
        }
        [Route("Wallet")]
        public IActionResult Wallet()
        {
            ViewBag.ListWallet = _userServics.GetWalletUser(User.Identity.Name);
            return View();
        }
        [Route("Wallet/Wallet")]
        [HttpPost]
        public IActionResult Wallet(ChargeWalletViewModel charge)
        {
            string username = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                ViewBag.ListWallet = _userServics.GetWalletUser(username);
                return View(charge);
            }
           int walletId= _userServics.ChargeWallet(username, "شارژ حساب", charge.Amount);
            #region onlinePayment
            var payment =new ZarinpalSandbox.Payment(charge.Amount);
            var res = payment.PaymentRequest("شارژ حساب", "https://localhost:44353/OnlinePayment/" + walletId,"rezakazazy8@yahoo.com");

            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            #endregion

            return View();
        }
    }
}
