using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MettSite.Startup))]
namespace MettSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
