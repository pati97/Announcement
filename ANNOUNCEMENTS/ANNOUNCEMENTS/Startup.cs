using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ANNOUNCEMENTS.Startup))]
namespace ANNOUNCEMENTS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
