using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EPayment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EPayment.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public async Task< ActionResult> Index()
        {
        

            var email = "seaagull@hotmail.com";
            var user = await this.UserManager.FindByEmailAsync(email);
         
            if (user == null)
            {
                user = new AspNetUser()
                {

                    UserName = email,
                    Email = email,
                    NationalId = "123456789",
                    Code = 1234,
                    
                };
                await this.UserManager.CreateAsync(user, "Fr33tim3#");
            }
            else
            {
                var result = await this.SignInManager.PasswordSignInAsync(user.Email, "Fr33tim3#", true, false);
                if (result == SignInStatus.Success)
                {
                    return Content("Hello " + user.UserName);
                }
            }

            return Content("Hello Index");
        }
    }
}