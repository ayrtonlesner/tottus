using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LocalIntranet.Startup))]
namespace LocalIntranet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
