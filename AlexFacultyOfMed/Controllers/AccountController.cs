using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AlexFacultyOfMed.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AlexFacultyOfMed.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AccountController : BaseController
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null)
                if (!user.EmailConfirmed)
                    return RedirectToAction("EmailNotConfirmed", "Account");
            if (!ModelState.IsValid)
                return View(model);
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result =
                await
                    SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                // return RedirectToAction("Contact", "Home");
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, model.RememberMe});
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        // Email Not Confirmed
        [AllowAnonymous]
        public ActionResult EmailNotConfirmed()
        {
            return View();
        }

        //Resend Email Confirmation
        [AllowAnonymous]
        public ActionResult ResendConfirmationEmail()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResendConfirmationEmail(ResendConfirmationEmail model, string returnUrl)
        {
            var Code = 0;


            try
            {
                Code = BitConverter.ToInt32(Convert.FromBase64String(model.Code + "=="), 0);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "الكود غير صحيح");
                return View(model);
            }
            if (Code == 0)
            {
                ModelState.AddModelError("", "الكود الذى قمت بإدخالة خاطىء");
                return View(model);
            }

            var _db = new ApplicationDbContext();

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("الكود غير مطابق الرقم القومى",
                    string.Format("عفوا البريد غير مسجل لدينا تأكد من عنوان البريد صحيح او قم بالتسجيل اولا  ",
                        model.NationalId));
                return View(model);
            }
            if (user.EmailConfirmed)
            {
                ModelState.AddModelError("البريد مسجل مسبقا", "عفوا هذا البريد قد قمت بتفعيلة مسبقا");
                return View(model);
            }

            var emp = await _db.Employees.FindAsync(Code);
            if (emp == null)
            {
                ModelState.AddModelError("الكود غير مطابق الرقم القومى",
                    "الرقم القومى لا يطابق الرمز الكودى برجاء مراجعة البيانات المدخلة");
                return View(model);
            }
            if (!((emp.Code == Code) && (emp.NationalId == model.NationalId) && (user.Email == model.Email)))
                ModelState.AddModelError("الكود غير مطابق الرقم القومى",
                    "الرقم القومى لا يطابق الرمز الكودى برجاء مراجعة البيانات المدخلة");

            if (!ModelState.IsValid)
                return View(model);


            await SendEmailCodeConfirmation(user, emp);

            return RedirectToAction("EmailSent", "Account", new {EmailAddress = model.Email});
        }

        [AllowAnonymous]
        public ActionResult EmailSent(string EmailAddress)
        {
            ViewBag.EmailAdd = EmailAddress;
            return View();
        }

        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
                return View("Error");
            return View(new VerifyCodeViewModel {Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result =
                await
                    SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe,
                        model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var Code = 0;
            Employee emp = null;
            try
            {
                Code = BitConverter.ToInt32(Convert.FromBase64String(model.Code + "=="), 0);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "الكود غير صحيح");
                return View(model);
            }


            var _db = new ApplicationDbContext();
            if (Code > 0)
                emp = await _db.Employees.FindAsync(Code);
            if (emp == null)
            {
                ModelState.AddModelError("", "الكود الذى قمت بإدخالة خاطىء");
                return View(model);
            }
            if (!((emp.Code == Code) && (emp.NationalId == model.NationalId)))
                ModelState.AddModelError("الكود غير مطابق الرقم القومى",
                    "الرقم القومى لا يطابق الرمز الكودى برجاء مراجعة البيانات المدخلة");


            if (ModelState.IsValid)
            {
                var user = new AspNetUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NationalId = model.NationalId,
                    Code = Code,
                    PhoneNumber = model.MobileNumber
                };
                var result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    var roleResult = await UserManager.AddToRoleAsync(user.Id, UserRoles.User);
                    //    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    await SendEmailCodeConfirmation(user, emp);

                    return RedirectToAction("EmailSent", "Account", new {EmailAddress = model.Email});
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private async Task SendEmailCodeConfirmation(AspNetUser user, Employee emp, string callBackUrl = null)
        {
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new {userId = user.Id, code},
                Request.Url.Scheme);


            var messageBody = "<html>";
            messageBody += "<head>";
            messageBody += "</head>";
            messageBody +=
                "<link href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css\" rel=\"stylesheet\">";
            messageBody += "</head>";
            messageBody +=
                "<body style=\"background-color: #fff; font-family: 'Andalus'; color: #2c3e50; font-size: 18px; direction: rtl; \">";
            messageBody +=
                "<img src= \"https://upload.wikimedia.org/wikipedia/ar/6/64/%D8%B7%D8%A8-%D8%A7%D8%B3%D9%83%D9%86%D8%AF%D8%B1%D9%8A%D8%A9.jpg \" style = \"width: 70px; height: 70px\" /> ";
            messageBody += "<div>جامعة الأسكندرية</div> ";
            messageBody += "<div>كلية الطب</div> ";
            messageBody += "<div><u>الوحدة الحسابية</u></div>";
            messageBody +=
                "<div style=\"background-color:#2c3e50;color:#fff  height:300px;  display:block; margin:10px; padding:30px;\">";
            messageBody +=
                "<div style=\"font-family: 'Andalus' color: #ccafaf; font-size:22px; font-weight:bold;\"> اهلا بك " +
                emp.Name + "<br /> <br /></div>";
            messageBody += "<div style=\"font-family: 'Arial' color: lightgray; font-size:18px;\">" +
                           "  لقد قمت بالتسجيل بصفحة كلية الطب جامعة الأسكندرية الخاصة بتحويلات الدفع الألكترونى لاتمام عملية التسجيل برجاء الضغط ";
            messageBody += "<a href=\"" + callbackUrl + "\" style=\"color:black;\">هنا</a>" + "<br>";
            messageBody +=
                " فى حالة حدوث اى صعوبات اثناء عملية التسجيل يرجى التوجة الى الوحدة الحسابية قسم المراجعة و شكرا </div>";
            messageBody += "</div>";
            messageBody += @"</body></html>";

            //   await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            await UserManager.SendEmailAsync(user.Id, "تأكيد تسجيل الحساب", messageBody);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if ((userId == null) || (code == null))
                return View("Error");
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if ((user == null) || !await UserManager.IsEmailConfirmedAsync(user.Id))
                    return View("ForgotPasswordConfirmation");
                if ((user.PhoneNumber != model.MobileNumber) || (user.Email != model.Email))
                    return View("ForgotPasswordConfirmation");
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                var _db = new ApplicationDbContext();
                var emp = await _db.Employees.SingleOrDefaultAsync(x => x.Code == user.Code);
                //   await  SendEmailCodeConfirmation(user, emp);

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code},
                    Request.Url.Scheme);
                await
                    UserManager.SendEmailAsync(user.Id, "أستعادة كلمة المرور",
                        "من فضلك قم بإستعادة كلمة المرور من الرابط التالى  <a href=\"" + callbackUrl + "\">أضغط هنا</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
                return View("Error");
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions =
                userFactors.Select(purpose => new SelectListItem {Text = purpose, Value = purpose}).ToList();
            return
                View(new SendCodeViewModel {Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
                return View("Error");
            return RedirectToAction("VerifyCode",
                new {Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe});
        }

        //
        // GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        //
        // POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new AspNetUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_userManager != null)
        //        {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        if (_signInManager != null)
        //        {
        //            _signInManager.Dispose();
        //            _signInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }


        public static class Base64Helper
        {
            private static readonly Regex _rx = new Regex(
                @"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}[AEIMQUYcgkosw048]=|[A-Za-z0-9+/][AQgw]==)?$",
                RegexOptions.Compiled);

            public static byte[] TryParse(string s)
            {
                if (s == null) throw new ArgumentNullException("s");

                if ((s.Length%4 == 0) && _rx.IsMatch(s))
                    try
                    {
                        return Convert.FromBase64String(s);
                    }
                    catch (FormatException)
                    {
                        // ignore
                    }
                return null;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                    properties.Dictionary[XsrfKey] = UserId;
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}