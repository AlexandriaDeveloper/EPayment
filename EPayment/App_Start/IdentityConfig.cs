using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPayment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace EPayment
{


    public class EpaymentUserManager :UserManager<AspNetUser>
    {
        public EpaymentUserManager(UserStore<AspNetUser> userStore ):base(userStore)
        {
            
        }

        public static EpaymentUserManager Create(IdentityFactoryOptions<EpaymentUserManager> options, IOwinContext context )
        {
            var userStore = new UserStore<AspNetUser>(context.Get<EPaymentContext>());
            return  new EpaymentUserManager(userStore);
        }


    }

    public class EPaymentSignInManager : SignInManager<AspNetUser, string>
    {
        public EPaymentSignInManager(EpaymentUserManager userManager, IAuthenticationManager authenticationManager):base(userManager,authenticationManager)
        {
            
        }

        public static EPaymentSignInManager Create(IdentityFactoryOptions<EPaymentSignInManager> options, IOwinContext context)
        {
          
            return new EPaymentSignInManager(context.Get<EpaymentUserManager>(),context.Authentication);
        }
    }

    public class IdentityConfig
    {

        //IUser Default User 
        //Store Container Hold Data On Memory
        //User Manager 











        //Server=tcp:erpdbalexfaculty.database.windows.net,1433;Initial Catalog=ERPDB;Persist Security Info=False;User ID=seagaull;Password=fr33tim3#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
    }
}