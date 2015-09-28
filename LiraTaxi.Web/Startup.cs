using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LiraTaxi.Web.Startup))]
namespace LiraTaxi.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
