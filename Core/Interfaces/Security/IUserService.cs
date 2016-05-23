using Core.DatabaseModels.Security;
using Core.DomainModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Security
{
    public interface IUserService : IDisposable
    {

        Task<TokenInfo> Authenticate(string email, string password);

        Task<TokenInfo> ValidateToken(string token);
    }
}
