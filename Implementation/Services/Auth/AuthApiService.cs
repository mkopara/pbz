using Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Auth
{
    public class AuthApiService : IAuthApiService
    {
        public Task<bool> ValidateToken(string token)
        {
            return Task.Run(() => true);
        }

        public Task<int> Authenticate(string email, string password)
        {
            return Task.Run(() => 1);
        }

   
    }
}
