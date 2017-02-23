using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlexFacultyOfMed.Controllers
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    using AlexFacultyOfMed.Models;
    using AlexFacultyOfMed.ViewModel;

    using Microsoft.AspNet.Identity;
    [Authorize]
    public class ComplainController : BaseController
    {
        // GET: Complain
        public async Task<ActionResult> Index()

        {


            var _db = new ApplicationDbContext();

            ComplainViewModel complains = new ComplainViewModel();
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            List<Complain> complains2 = null;


            if (User.IsInRole(UserRoles.Admin))
            {
                complains2 = _db.Complains.Where(x => x.Status == false).ToList();

            }
            else if (User.IsInRole(UserRoles.User) && !User.IsInRole(UserRoles.Admin))
            {
                complains2 = _db.Complains.Where(u => u.UserId == user.Id).ToList();

            }
            foreach (var complain in complains2)
            {
                complains.ComplainItemsViewModels.Add(new ComplainItemsViewModel()
                {
                    CreatedDate = complain.CreatedDate,
                    ComplainStatus = complain.Status,
                    ComplainId = complain.Id,
                    UserName = _db.Employees.FindAsync(UserManager.FindByIdAsync(complain.UserId).Result.Code).Result.Name
                });
            }
            complains.ComplainCounter = complains2.Count();


            return View(complains);
        }

        [HttpGet]
        public async Task<ActionResult> PostComplain()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> PostComplain(PostNewComplain model)
        {
            var _db = new ApplicationDbContext();

            if (!ModelState.IsValid)
            {
                return this.View(model);
            }


            _db.Complains.Add(new Complain()
            {
                Status = false,
                CreatedDate = DateTime.Now,
                UserId = User.Identity.GetUserId(),
                ComplainDetailses = new List<ComplainDetails>()
                                        {new ComplainDetails()
                                             {
                                            ComplainQ =System.Net.WebUtility.HtmlEncode( model.ComplainQ),
                                             ComplainQDate = DateTime.Now
                                             }
                                        }
            });
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> ComplainDetails(int id)
        {

            IList<ComplainDetailsViewModel> model = new List<ComplainDetailsViewModel>();
            var _db = new ApplicationDbContext();
            var result =await _db.ComplainDetailses.Include("Complain").Where(x => x.ComplainId == id).ToListAsync();
            var user = await UserManager.FindByIdAsync(result.FirstOrDefault().Complain.UserId);
            var empName =await _db.Employees.FindAsync(user.Code);
            foreach (var complainDetailse in result)
            {
                model.Add(new ComplainDetailsViewModel()
                {
                    ComplainDetailsId = complainDetailse.Id,
                    ComplainQ = (complainDetailse.ComplainQ),
                    ComplainQDate = complainDetailse.ComplainQDate,
                    ComplainA = (complainDetailse.ComplainA),
                    ComplainADate = complainDetailse.ComplainADate,
                    AskedBy =  empName.Name


                });
            }
            ViewBag.ComplainId = id;

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ComplainDetails(ComplainDetailsViewModel model)
        {
            return null;
        }
        [HttpGet]

        public async Task<ActionResult> Reply(int Id)
        {
            if (!User.IsInRole(UserRoles.Admin))
            {
                return RedirectToAction("Index");
            }

            var _db = new ApplicationDbContext();
            var result = await _db.ComplainDetailses.FindAsync(Id);
            var model = new ComplainDetailsViewModel()
            {
                ComplainDetailsId = result.Id,
                ComplainId = result.ComplainId,
                ComplainA = ( result.ComplainA),
                ComplainADate = result.ComplainADate,
                ComplainQDate = result.ComplainQDate,
                ComplainQ = (result.ComplainQ)
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public async Task<ActionResult> Reply(ComplainDetailsViewModel model)
        {
            if (!User.IsInRole(UserRoles.Admin))
            {
                return RedirectToAction("Index");
            }
            var _db = new ApplicationDbContext();
            var result = await _db.ComplainDetailses.FindAsync(model.ComplainDetailsId);
            result.ComplainA = System.Net.WebUtility.HtmlEncode( model.ComplainA);
            result.ComplainADate = DateTime.Now;

            if (!ModelState.IsValid) {
                return View(model);
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]


        public async Task<ActionResult> CloseComplain(int Id)
        {
            if (!User.IsInRole(UserRoles.Admin))
            {
                return RedirectToAction("Index");
            }
            var _db = new ApplicationDbContext();
            var result = await _db.Complains.FindAsync(Id);
            result.Status = true;
            result.ClosedDate = DateTime.Now;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}