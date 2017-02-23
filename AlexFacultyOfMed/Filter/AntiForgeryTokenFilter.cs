using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlexFacultyOfMed.Filter
{
    using AlexFacultyOfMed.Helper;

    public class AntiForgeryTokenFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //var s = filterContext.Exception.GetType();
            //var message = filterContext.Exception.Data;
            //var exceptionCode = filterContext.Exception.InnerException;

            //if (filterContext.Exception.GetType() == typeof(HttpAntiForgeryException))
            //{
            //    filterContext.Result = new RedirectResult("/Error/ForgeryToken"); // whatever the url that you want to redirect to
            //    filterContext.ExceptionHandled = true;
            //}
           Logger.Log(filterContext.Exception);

            if (filterContext.Exception.GetType() == typeof(System.Web.HttpRequestValidationException))
            {
                filterContext.Result = new RedirectResult("/Error/ForgeryToken"); // whatever the url that you want to redirect to
                filterContext.ExceptionHandled = true;

            }
        }
    }
}