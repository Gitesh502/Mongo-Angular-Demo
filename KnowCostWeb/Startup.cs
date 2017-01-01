using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KnowCostWeb.Startup))]
namespace KnowCostWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);


        }
    }
}
