using AuthApi.Attributes;
using AuthApi.Models;
using AuthAtrributes;
using Core.DatabaseModels.Security;
using Core.DomainModels.Security;
using Core.Interfaces;
using Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace AuthApi.Controllers
{
    public class AuthController : ApiController
    {
        IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("api/auth/generate")]
        [LocalAuth]
        [HttpPost]
        public async Task<HttpResponseMessage> GenerateToken([FromBody]AuthModel model)
        {
            if (model == null) model = new AuthModel();
            using (_userService)
            {
                if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
                {
                    var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                    {
                        if (basicAuthenticationIdentity.TokenInfo != null)
                            return Request.CreateResponse(HttpStatusCode.OK, basicAuthenticationIdentity.TokenInfo);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        [Route("api/auth/validate")]
        [HttpPost]
        public async Task<HttpResponseMessage> ValidateToken([FromBody]AuthModel model)
        {
            //if (model == null) model = new AuthModel();
            //using (_userService)
            //{

            //    if(model.Token == null)
            //        return Request.CreateResponse(HttpStatusCode.Unauthorized, new List<string> { "Invalid token" });

            //    var dbToken = await _userService.GetTokenInfo(model.Token);

            //    if (dbToken == null)
            //        return Request.CreateResponse(HttpStatusCode.Unauthorized, new List<string> { "Invalid token" });
            //    if (dbToken.Expired)
            //        return Request.CreateResponse(HttpStatusCode.Unauthorized, new List<string> { "Token Expired on " + dbToken.Expires });

                return Request.CreateResponse(HttpStatusCode.OK);

            

        }

    }
}
