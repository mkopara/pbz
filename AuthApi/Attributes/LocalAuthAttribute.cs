using AuthAtrributes;
using Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace AuthApi.Attributes
{
    //attribute for local authentication
    public class LocalAuthAttribute : GenericAuthenticationFilter
    {

        /// <summary>
        /// Default Authentication Constructor
        /// </summary>
        public LocalAuthAttribute()
        {
        }

        /// <summary>
        /// AuthenticationFilter constructor with isActive parameter
        /// </summary>
        /// <param name="isActive"></param>
        public LocalAuthAttribute(bool isActive)
            : base(isActive)
        {
        }

        /// <summary>
        /// Protected overriden method for authorizing user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override async Task<bool> OnAuthorizeUserAsync(string username, string password, HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration
                               .DependencyResolver.GetService(typeof(IUserService)) as IUserService;
            if (provider != null)
            {
                try
                {
                    var tokenInfo = await provider.Authenticate(username, password);
                    if (tokenInfo != null)
                    {
                        var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                        if (basicAuthenticationIdentity != null)
                            basicAuthenticationIdentity.TokenInfo = tokenInfo;
                        
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return false;
        }
    }
}