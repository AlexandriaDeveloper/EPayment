using System.Web.Mvc;

namespace AlexFacultyOfMed
{
    using Filter;
    using System.Web.Helpers;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
         //   filters.Add(new RequireHttpsAttribute());
         filters.Add(new AntiForgeryTokenFilter());
            //AntiForgeryConfig.RequireSsl = true;
        }
    }
}