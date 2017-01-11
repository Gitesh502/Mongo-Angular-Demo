using KnowCostData.Repository;
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
           () => new ChatAppHub(new ChatRepository(),new UserRepository()));
         
            app.MapSignalR();
            ConfigureAuth(app);


        }
    }
}
