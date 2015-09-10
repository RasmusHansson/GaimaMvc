using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GaimaMvc.Startup))]
namespace GaimaMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
