using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WCFAuthenticationClient.Startup))]
namespace WCFAuthenticationClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
