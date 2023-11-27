using Dto.Payment;
using EndPoint.Site.Utilities;
using EndPoint.SiteMain.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.CartServices;
using Store.Application.Services.Finance.Command.AddRequestPay;
using Store.Application.Services.Finance.Query.GetRequestPay;
using Store.Application.Services.Orders.Command.AddNewOrder;
using ZarinPal.Class;

namespace EndPoint.SiteMain.Controllers
{
    [Authorize("Customer")]
    public class PayController : Controller
    {
        private readonly IAddRequestPay _addRequestPay;
        private readonly ICartServices _cartServices;
        private readonly IGetRequestPay _getRequestPay;
        private readonly IAddNewOrder _addNewOrder;
        private readonly CookiesManeger _cookiesManeger;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        public PayController(
            IAddRequestPay addRequestPay, 
            ICartServices cartServices,
            IGetRequestPay getRequestPay,
            IAddNewOrder addNewOrder
            )
        {
            _addRequestPay = addRequestPay;
            _cartServices = cartServices;
            _getRequestPay = getRequestPay;
            _addNewOrder = addNewOrder;
            _cookiesManeger = new CookiesManeger();
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }

        public async Task<IActionResult> Index()
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartServices.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext),
                UserId);
            if(cart.Data.SumAmount > 0)
            {
                var requestPay = _addRequestPay.Execute(cart.Data.SumAmount, UserId.Value);
                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09152221648",
                    CallbackUrl = $"https://localhost:44363/Pay/Verify?guid={requestPay.Data.guid}",
                    Description = "رضا مشکی ثانی" + requestPay.Data.RequestPayId,
                    Email = requestPay.Data.Email,
                    Amount = requestPay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
            }
            else
            {
                RedirectToAction("Index", "Cart");
            }
            return View();
        }
        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {
            var requestPay = _getRequestPay.Execute(guid);
            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = requestPay.Data.Amount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);

            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartServices.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            if (verification.Status == 100)
            {
                _addNewOrder.Execute(new RequestAddNewOrderSericeDto
                {
                    CartId = cart.Data.CartId,
                    UserId = UserId.Value,
                    RequestPayId = requestPay.Data.Id
                });

                //redirect to orders
                return RedirectToAction("Index", "Orders");
            }
            else
            {

            }

            return View(); ;
        }
    }
}
