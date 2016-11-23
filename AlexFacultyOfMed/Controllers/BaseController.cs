using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace AlexFacultyOfMed.Controllers
{
    public class BaseController : Controller
    {
        private EmailService.ApplictaionRoleManager _roleManager;
        private EmailService.ApplicationSignInManager _signInManager;
        private EmailService.ApplicationUserManager _userManager;


        public EmailService.ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<EmailService.ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public EmailService.ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<EmailService.ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public EmailService.ApplictaionRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<EmailService.ApplictaionRoleManager>(); }
            private set { _roleManager = value; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}