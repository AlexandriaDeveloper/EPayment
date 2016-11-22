using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FacultyOfMed.Startup))]
namespace FacultyOfMed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
