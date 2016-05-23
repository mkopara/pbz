using Core.DomainModels.Security;
using Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AuthAtrributes
{
    public class TokenAuthorizationFilter : ActionFilterAttribute
    {
        private const string Token = "Token";

        public override async Task OnActionExecutingAsync(HttpActionContext filterContext, CancellationToken cancellationToken)
        {
            //  Get API key provider
            var provider = filterContext.ControllerContext.Configuration
            .DependencyResolver.GetService(typeof(IAuthApiService)) as IAuthApiService;

            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();

                TokenInfo token = await provider.ValidateToken(tokenValue);
                // Validate Token
                if (tokenValue == null)
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                    filterContext.Response = responseMessage;
                }
                if(token == null)
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Token expired or does not exist" };
                    filterContext.Response = responseMessage;
                }

            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(filterContext);

        }
    }
}
