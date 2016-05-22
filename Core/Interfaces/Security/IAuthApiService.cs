using Core.DatabaseModels.Security;
using Core.DomainModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Security
{
    public interface IAuthApiService
    {
 
        Task<bool> ValidateToken(string token);

        Task<int> Authenticate(string email, string password);

    }
}
