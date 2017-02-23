using System.Web.Mvc;
using System.Web.Routing;

namespace AlexFacultyOfMed
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //        routes.MapRoute(
            //"Empty",
            //"",
            //new { controller = "Home", action = "Index", }
            //);

            var defaultRoute =
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });


            //   var route = routes.MapRoute(
            //name: "Dynamic",
            //url: "{name}",
            //defaults: new { controller = "Dynamic", action = "Index" });

            //   defaultRoute = route;
        }
    }
}