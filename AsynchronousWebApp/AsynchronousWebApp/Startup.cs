using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AsynchronousWebApp.Startup))]
namespace AsynchronousWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
