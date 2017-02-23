using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AlexFacultyOfMed.Models;
using AlexFacultyOfMed.ViewModel.AdminVM;

namespace AlexFacultyOfMed.Controllers
{
    using System.Collections.Generic;
    using System.Web.Security;

    using AlexFacultyOfMed.ViewModel;

    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.PowerUser)]
    //[RequireHttps]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult Index(IndexVm model)
        {
            if (ModelState.IsValid)
                return  RedirectToAction("UserDetails", new {model.NationalId});
            return View(model);
        }

        public async Task<ActionResult> UserDetails(IndexVm model)
        {
            AspNetUser user = null;

            if (model.NationalId == null)
                return View("Error");
            if (ModelState.IsValid)
            {
                var context = ApplicationDbContext.Create();

                user =await context.Users.SingleOrDefaultAsync(x => x.NationalId == model.NationalId);
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
            if(model.NationalId==null)
                return View("Error");


            var user = await context.Users.SingleOrDefaultAsync(x => x.NationalId == model.NationalId);
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

          
            
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Contact( string NationalId, short Page = 1)
        {
            //var identity = (ClaimsIdentity)User.Identity;
            //var code = int.Parse( identity.Claims.FirstOrDefault(c => c.Type == "CodeClaim").Value);
            if (string.IsNullOrEmpty(NationalId))
                return View("Error");






            short pageSize = 20;
            var _db = new ApplicationDbContext();

            var user = await UserManager.GetUserByNationalIdAsync(NationalId);
            if (user == null)
                return View("Error");
            var result = new PaymentInfoVM
            {
                PaymentData = await (from dfd in _db.DailyFileDetailses
                                     join df in _db.DailyFiles on dfd.DailyFileId equals df.Id
                                     join d in _db.Dailies on df.DailyId equals d.Id
                                     where (dfd.EmployeeId == user.Code)
                                           && (d.Open == false)
                                     select new PaymentData
                                     {
                                         DailyFileDetails = dfd,
                                         DailyFile = df,
                                         Daily = d
                                     }).ToListAsync(),
                Employee = await (from e in _db.Employees
                                  where e.Code == user.Code
                                  select e).FirstOrDefaultAsync()
            };


            result.CurrentPage = Page;
            result.Pages = result.PaymentData.Count / pageSize;
            if (result.PaymentData.Count % pageSize != 0)
                result.Pages += 1;


            result.PaymentData = result.PaymentData.Skip((Page - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.DailyFileDetails.Id)
                .ToList();
            if (result.Employee == null)
            {
                result.Employee = new Employee();
            }


            return View(result);
        }

        [Authorize]
        public async Task<ActionResult> FileData(int id)
        {
            // var identity = (ClaimsIdentity)User.Identity;
            //var code = int.Parse( identity.Claims.FirstOrDefault(c=>c.Type=="CodeClaim").Value);

         //   var user = await UserManager.FindByNameAsync(User.Identity.Name);
            ApplicationDbContext db = new ApplicationDbContext();

            var f = from c in db.DailyFileDetailses where c.Id == id select c;
            if (f.Count() == 0)
            {
                return View("Error");
            }
            //if (f.FirstOrDefault().EmployeeId != user.Code)
            //{
            //    return this.Content("ليس لديك الصلاحية لدخول هذة الصفحة ");
            //}

            var result = await (from c in db.DailyFileDetailsDatas
                                where c.DailyFileDetailsId == id
                                join a in db.Accounts on c.AccountId equals a.Id
                                select new
                                {
                                    AccountId = c.AccountId,
                                    Id = c.Id,
                                    AccountName = a.Name,
                                    AccountValue = c.Value
                                }).ToListAsync();

            List<DataInfo> model = new List<DataInfo>();

            foreach (var VARIABLE in result)
            {
                model.Add(new DataInfo()
                {
                    Id = VARIABLE.Id,
                    AccountName = VARIABLE.AccountName,
                    AccountValue = VARIABLE.AccountValue,
                    AccountId = VARIABLE.AccountId.ToString()
                }

                    );
            }

            model = model.OrderByDescending(x => x.Id).ToList();
            return this.View(model);
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