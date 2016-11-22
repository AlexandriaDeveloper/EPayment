using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AlexFacultyOfMed.Models;
using AlexFacultyOfMed.ViewModel;

namespace AlexFacultyOfMed.Controllers
{
    [RequireHttps]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Contact(short Page = 1)
        {
            short pageSize = 20;
            var _db = new ApplicationDbContext();
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return View("Error");
            var result = new PaymentInfoVM
            {
                PaymentData = (from dfd in _db.DailyFileDetailses
                    join df in _db.DailyFiles on dfd.DailyFileId equals df.Id
                    join d in _db.Dailies on df.DailyId equals d.Id
                    where (dfd.EmployeeId == user.Code)
                          && (d.Open == false)
                    select new PaymentData
                    {
                        DailyFileDetails = dfd,
                        DailyFile = df,
                        Daily = d
                    }).ToList(),
                Employee = (from e in _db.Employees
                    where e.Code == user.Code
                    select e).FirstOrDefault()
            };
            result.CurrentPage = Page;
            result.Pages = result.PaymentData.Count/pageSize;
            if (result.PaymentData.Count%pageSize != 0)
                result.Pages += 1;


            result.PaymentData = result.PaymentData.Skip((Page - 1)*pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.DailyFileDetails.Id)
                .ToList();

            return View(result);
        }

        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]
        public ActionResult Test()
        {
            return Content("This Is My Secret Page");
        }
    }

    public static class UserRoles
    {
        public const string Admin = "AdminUser";
        public const string PowerUser = "PowerUser";
        public const string User = "User";
    }
}