using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlexFacultyOfMed.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult HttpError404(string error)
        {
            ViewData["Title"] = "عفوا الصفحة  غير موجودة ";
        //    ViewData["Description"] = error;
            return View("Index");
        }

        public ActionResult HttpError500(string error)
        {
            ViewData["Title"] = "عفوا حدث خطأ اثناء العملية ";
      //      ViewData["Description"] = error;
            return View("Index");
        }

        public ActionResult ForgeryToken(string error)
        {
            ViewData["Title"] = "عفوا غير مسموح بكتابة اى حروف خاصة  ";
            //      ViewData["Description"] = error;
            return View("Index");
        }
        public ActionResult General(string error)
        {
            ViewData["Title"] = "عفوا حدث خطأ اثناء العملية";
          //  ViewData["Description"] = error;
            return View("Index");
        }
    }

}