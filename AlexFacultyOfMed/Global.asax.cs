using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AlexFacultyOfMed
{
    using Controllers;
    using Microsoft.Owin.Logging;
    using System;
    using System.Web.Security;

    using AlexFacultyOfMed.Helper;
    using AlexFacultyOfMed.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["UsersOnline"] = 0;
        }
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("X-Powered-By");
            this.Response.AppendHeader("X-Frame-Options", "DENY");
          //  Response.AddHeader("Strict-Transport-Security", "max-age=300");
            Response.AddHeader("X-Frame-Options", "SAMEORIGIN");
        }

        protected void Application_BeginRequest()
        {

            


            //switch (Request.Url.Scheme)
            //{
            //    case "https":
            //        Response.AddHeader("Strict-Transport-Security", "max-age=300");
            //        break;
            //    case "http":
            //        var path = @"https://" +this.Request.Url.Host + this.Request.Url.PathAndQuery;
            //        Response.Status = "301 Moved Permanently";
            //        Response.AddHeader("Location", path);
            //        break;
            //}
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Response.Cookies.Count > 0)
            {
                foreach (string s in Response.Cookies.AllKeys)
                {
                    if (s == FormsAuthentication.FormsCookieName || s.ToLower() == "asp.net_sessionid")
                    {
                        Response.Cookies[s].Secure = true;
                    }
                }
            }
        }





        protected void Application_Error(object sender, EventArgs e)
        {

            Exception exception = Server.GetLastError();
            // Get and Log the exception
            Logger.Log(Server.GetLastError());
            // Clear the exception
            Server.ClearError();
            // Transfer the user to Errors.aspx page
            //Server.Transfer("Errors.aspx");

            Response.Clear();


            HttpException httpException = exception as HttpException;
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else //It's an Http Exception, Let's handle it.
            {

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // Page not found.
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 500:
                        // Server error.
                        routeData.Values.Add("action", "HttpError500");
                        break;
                    // Here you can handle Views to other error codes.
                    // I choose a General error template  
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
            }

            // Pass exception details to the target error View.
            routeData.Values.Add("error", exception);

            // Clear the error on server.
            Server.ClearError();

            // Call target Controller and pass the routeData.
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(
           new HttpContextWrapper(Context), routeData));

        }
    }
}