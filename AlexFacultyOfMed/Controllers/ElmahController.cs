using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlexFacultyOfMed.Controllers
{
    using AlexFacultyOfMed.Helper;
    [Authorize(Roles = UserRoles.Admin)]
    //[RequireHttps]
    // GET: Elmah
    public class ElmahaController : Controller
    {
   

        public ActionResult Index(string type)
            {
                return new ElmahResult(type);
            }
        }
  
}