using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BillPayingWebsite.Startup))]
namespace BillPayingWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
