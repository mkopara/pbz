using Core.DatabaseModels.Security;
using Core.Interfaces;
using Core.Interfaces.Security;
using Implementation.Databases.Security;
using Implementation.Repositories;
using Implementation.UnitOfWork.Security;
using Implementation.User.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity.WebApi;


namespace AuthApi.App_Start
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
        
            
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IRepository<User>, Repository<User>>();
            container.RegisterType<IRepository<UserToken>, Repository<UserToken>>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<DbContext, GalileoSecurityContext>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}