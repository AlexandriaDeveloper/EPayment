using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPayment.Controllers
{
    public abstract class BaseController : Controller
    {
        public EpaymentUserManager UserManager { get { return HttpContext.GetOwinContext().Get<EpaymentUserManager>(); }}
        public EPaymentSignInManager SignInManager { get { return HttpContext.GetOwinContext().Get<EPaymentSignInManager>(); } }
    }
}