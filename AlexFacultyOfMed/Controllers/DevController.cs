using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlexFacultyOfMed.Controllers
{
    using System.Web.Security;

    using AlexFacultyOfMed.Models;
    using AlexFacultyOfMed.ViewModel.Dev;

    public class DevController : BaseController
    {
        // GET: Dev
        public ActionResult Index()
        {
            var appVar = HttpContext.Application["UsersOnline"];
            DevIndexViewModel model = new DevIndexViewModel();
            var _db = new ApplicationDbContext();
            //if (!string.IsNullOrEmpty(appVar))
            //{
            //    model.OnlineMember = int.Parse(appVar);
            //}

            model.OnlineMember = Session.Count;

            model.RegisterdMembersNumber = _db.Users.Count();

            return View(model);
        }
    }
}