using Core.DatabaseModels.Security;
using Core.DomainModels;
using Core.Interfaces;
using Core.Interfaces.ExchangeRate;
using Core.Interfaces.Security;
using Implementation.Databases.Security;
using Implementation.Repositories;
using Implementation.UnitOfWork.Security;
using Implementation.User.Services;
using Microsoft.Practices.Unity;
using System.Data.Entity;
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

            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<DbContext, GalileoSecurityContext>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<User>, Repository<User>>();
            container.RegisterType<IRepository<UserToken>, Repository<UserToken>>();
            container.RegisterType<IUserService, UserService>();
          

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}