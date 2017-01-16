using BusinessService.Services;
using KnowCostWeb.Helpers;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KnowCostWeb.Startup))]
namespace KnowCostWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(
           typeof(ChatAppHub),
           () => new ChatAppHub(new UserService()));
            var idProvider = new CustomUserIdProvider();
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);          

            
            ConfigureAuth(app);
            app.MapSignalR();


        }
    }
}
