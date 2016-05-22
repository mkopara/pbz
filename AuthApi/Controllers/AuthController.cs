using AuthApi.Models;
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
        [HttpPost]
        public async Task<HttpResponseMessage> GenerateToken([FromBody]AuthModel model)
        {
            if (model == null) model = new AuthModel();
            using (_userService)
            {
                //var tokena = await _userService.GetUserAsync(1);

                //var testUser = new CreateUser
                //{
                //    Email = "robiskaro@gmail.com",
                //    Password = "test22",
                //    PhoneNumber = "9032023"
                //};

               await  _userService.ValidateToken("ce8625ab-cf4b-496d-b1ad-55941eff2fdf");

                if(model.Password == null || model.Email == null)
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new List<string> { "Please supply credentials" });

                var token = await _userService.Authenticate(model.Email, model.Password);
               
                if (token != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, token);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new List<string> { "Invalid credentials" });
                }
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
