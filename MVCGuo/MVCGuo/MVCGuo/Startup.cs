using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCGuo.Startup))]
namespace MVCGuo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
