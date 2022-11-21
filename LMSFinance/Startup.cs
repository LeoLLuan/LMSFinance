using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LMSFinance.Startup))]
namespace LMSFinance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
