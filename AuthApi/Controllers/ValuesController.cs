using Core.DatabaseModels.Security;
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
    public class ValuesController : ApiController
    {
        IUserService _userService;
        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {

            using (_userService)
            {
                var tokena = await _userService.GetUserAsync(1);

                return new string[] { "value1", "value2" };
            }

        }

    }
}
