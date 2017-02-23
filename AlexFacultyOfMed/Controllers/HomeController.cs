using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AlexFacultyOfMed.Models;
using AlexFacultyOfMed.ViewModel;

namespace AlexFacultyOfMed.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Security.Claims;

    //[RequireHttps]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Contact(short Page = 1)
        {
            //var identity = (ClaimsIdentity)User.Identity;
            //var code = int.Parse( identity.Claims.FirstOrDefault(c => c.Type == "CodeClaim").Value);



            



            short pageSize = 20;
            var _db = new ApplicationDbContext();

            var user = await UserManager.FindByNameAsync(User.Identity.Name);
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
                                  where e.Code ==user.Code
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

            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            ApplicationDbContext db = new ApplicationDbContext();

            var f = from c in db.DailyFileDetailses where c.Id == id select c;
            if (f.Count() == 0) {
                return View("Error");
            }
            if (f.FirstOrDefault().EmployeeId != user.Code)
            {
                return this.Content("ليس لديك الصلاحية لدخول هذة الصفحة ");
            }

            var result = await (from c in db.DailyFileDetailsDatas
                                where c.DailyFileDetailsId == id
                                join a in db.Accounts on c.AccountId equals a.Id
                                select new {AccountId=c.AccountId,
                                Id=c.Id,
                                    AccountName = a.Name,
                                    AccountValue = c.Value }).ToListAsync();

            List<DataInfo> model = new List<DataInfo>();
            
            foreach (var VARIABLE in result)
            {
                model.Add(new DataInfo()
                {
                    Id=VARIABLE.Id,
                    AccountName = VARIABLE.AccountName,
                    AccountValue = VARIABLE.AccountValue,
                    AccountId =  VARIABLE.AccountId.ToString()
                }

                    );
            }

            model = model.OrderByDescending(x => x.Id).ToList();
            return this.View(model);
        }

        [AllowAnonymous]
        public ActionResult Error(string aspxerrorpath)
        {


            return this.View("Error");
        }
    }




    public static class UserRoles
    {
        public const string Admin = "AdminUser";
        public const string PowerUser = "PowerUser";
        public const string User = "User";
    }

    public class DataInfo
    {
        public string AccountName { get; set; }
        public string AccountId { get; set; }
        public string AccountValue { get; set; }

        public int Id { get; set; }
    }

}