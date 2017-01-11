using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using KnowCostData.Repository;
using System.Web.Http;
using KnowCostWeb.Controllers;

namespace KnowCostWeb
{
    public static class UnityConfig
    {
        public static IUnityContainer RegisterComponents()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        public static IUnityContainer BuildUnityContainer()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IChatRepository, ChatRepository>();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            return container;
        }
    }
}