using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

using System.Web.Http;
using KnowCostWeb.Controllers;
using BusinessService.Services;
using KnowCostWeb.Helpers;

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
            container.RegisterType<HomeController>(new InjectionConstructor());
            container.RegisterType<CustomUserIdProvider>(new InjectionConstructor());
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUserMessageService, UserMessageService>();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            return container;
        }
    }
}