using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(l10_test.Startup))]
namespace l10_test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


        }
    }
}
