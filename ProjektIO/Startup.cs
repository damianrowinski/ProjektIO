using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektIO.Startup))]
namespace ProjektIO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


        }
    }
}
