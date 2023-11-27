using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.CodeAnalysis.Scripting;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Common.Dto;
using System.Text.RegularExpressions;
using EndPoint.SiteMain.Areas.Admin.Models.ViewModels.AuthenticationViewModel;
using Store.Application.Services.Users.Commands.UserLogin;

namespace EndPoint.SiteMain.Controllers
{
    public class AuthenticationController : Controller
    {

        #region Injection
        private readonly IRegisterUserService _registerUserService;
        private readonly IUserLoginService _userLoginService;
        public AuthenticationController(IRegisterUserService registerUserService,
            IUserLoginService userLoginService
            )
        {
            _registerUserService = registerUserService;
            _userLoginService = userLoginService;

        }
        #endregion

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel request)
        {
            #region validation
            if (string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.RePassword))
            {
                return Json(new ResultDto { IsSuccess = false, Message = "لطفا تمامی موارد رو ارسال نمایید" });
            }

            if (User.Identity.IsAuthenticated == true)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "شما به حساب کاربری خود وارد شده اید! و در حال حاضر نمیتوانید ثبت نام مجدد نمایید" });
            }
            if (request.Password != request.RePassword)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور و تکرار آن برابر نیست" });
            }
            if (request.Password.Length < 3)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "رمز عبور باید حداقل 8 کاراکتر باشد" });
            }

            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

            var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return Json(new ResultDto { IsSuccess = false, Message = "ایمیل خودرا به درستی وارد نمایید" });
            }
            #endregion

            var signeupResult = _registerUserService.Execute(new RequestRegisterUserDto
            {
                Email = request.Email,
                FullName = request.FullName,
                Password = request.Password,
                RePassword = request.RePassword,
                Roles = new List<RolesRegisterUserDto>()
                {
                     new RolesRegisterUserDto { Id = 3},
                }
            });

            if (signeupResult.IsSuccess == true)
            {
                #region setClaim
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,signeupResult.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Email, request.Email),
                    new Claim(ClaimTypes.Name, request.FullName),
                    new Claim(ClaimTypes.Role, "Customer"),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };
                HttpContext.SignInAsync(principal, properties);
                #endregion
            }
            return Json(signeupResult);
        }

        #region Mytest
        public IActionResult Index()
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword("123456789");
            bool verified = BCrypt.Net.BCrypt.Verify("123456789", passwordHash);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "30"),
                new Claim(ClaimTypes.Name, "RezaMSani"),
                new Claim(ClaimTypes.Email, "rezamsanitrrga@gmail.com"),
                new Claim(ClaimTypes.MobilePhone, "09152221648"),

            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            HttpContext.SignInAsync(principal, properties);
            var name = User.Identity.Name;
            var email = User.FindFirst(ClaimTypes.Email);
            var mobile = User.FindFirst(ClaimTypes.MobilePhone);
            return Content("testAuth");
        }
        #endregion

        public IActionResult Signin(string ReturnUrl = "/")
        {
            ViewBag.url = ReturnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Signin(string Email, string Password, string url = "/")
        {
            var signupResult = _userLoginService.Execute(Email, Password);
            if (signupResult.IsSuccess == true)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,signupResult.Data.UserId.ToString()),
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Name, signupResult.Data.Name)
            };
                foreach (var item in signupResult.Data.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(signupResult);
        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
