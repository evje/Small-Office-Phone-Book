using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SOPB.WebUI.Startup))]
namespace SOPB.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
