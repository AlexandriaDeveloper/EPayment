using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AlexFacultyOfMed.Models;
using AlexFacultyOfMed.ViewModel.AdminVM;

namespace AlexFacultyOfMed.Controllers
{
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.PowerUser)]
    [RequireHttps]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(IndexVm model)
        {
            if (ModelState.IsValid)
                return  RedirectToAction("UserDetails", new {model.NationalId});
            return View(model);
        }

        public async Task<ActionResult> UserDetails(IndexVm model)
        {
            AspNetUser user = null;


            if (ModelState.IsValid)
            {
                var context = ApplicationDbContext.Create();

                user = context.Users.SingleOrDefault(x => x.NationalId == model.NationalId);
                if (user == null)
                {
                    ModelState.AddModelError("", "تأكد من ان الرقم القومى مسجل");
                    return View("Index");
                }
                return View(new UserDetailsVM
                {
                    NationalId = user.NationalId,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    LockoutEnabled = user.LockoutEnabled,
                    LockoutEndDateUtc = user.LockoutEndDateUtc
                });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserDetails(UserDetailsVM model)
        {
            var context = ApplicationDbContext.Create();

            var user = context.Users.SingleOrDefault(x => x.NationalId == model.NationalId);
                //await UserManager.GetUserByNationalIdAsync(model.NationalId);


            if (user == null)
            {
                ModelState.AddModelError("", "الرقم القومى المطلوب غير مسجل");
                return View(model);
            }
            user.UserName = model.Email;

            user.Email = model.Email;
            user.EmailConfirmed = model.EmailConfirmed;
            user.PhoneNumber = model.PhoneNumber;
            user.EmailConfirmed = model.EmailConfirmed;
            user.LockoutEnabled = model.LockoutEnabled;
            user.LockoutEndDateUtc = model.LockoutEndDateUtc;
            try
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (!ModelState.IsValid)

                return View(model);
        }

        public async Task<ActionResult> ResendPass(string NationalId)
        {
            var user = await UserManager.GetUserByNationalIdAsync(NationalId);
            if (user == null)
                ModelState.AddModelError("", "تأكد من ان الرقم القومى مسجل لديك");
            if (!ModelState.IsValid)
                return View(NationalId);
            var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code}, Request.Url.Scheme);
            await
                UserManager.SendEmailAsync(user.Id, "أستعادة كلمة المرور",
                    "من فضلك قم بإستعادة كلمة المرور من الرابط التالى  <a href=\"" + callbackUrl + "\">أضغط هنا</a>");
            // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            return RedirectToAction("EmailSent", "Account", new {EmailAddress = user.Email});
        }
    }
}