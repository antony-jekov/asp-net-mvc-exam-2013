using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCExam.Web.Startup))]

namespace MVCExam.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}