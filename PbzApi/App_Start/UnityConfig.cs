using Core.DomainModels;
using Core.Interfaces;
using Core.Interfaces.ExchangeRate;
using Implementation.Repositories;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace PbzApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //registering corresponding repository for IPBZExchangeRateRepository
            container.RegisterType<IPBZExchangeRateRepository, PBZExchangeRateRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}