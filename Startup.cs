using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyNearInv.Startup))]
namespace MyNearInv
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
