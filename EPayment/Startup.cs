using System;
using System.Threading.Tasks;
using EPayment.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(EPayment.Startup))]

namespace EPayment
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.CreatePerOwinContext(EPaymentContext.Create);
            app.CreatePerOwinContext<EpaymentUserManager>(EpaymentUserManager.Create);
            app.CreatePerOwinContext<EPaymentSignInManager>(EPaymentSignInManager.Create);
        }
    }
}
