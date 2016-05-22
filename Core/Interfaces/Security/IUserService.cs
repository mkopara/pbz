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
        Task<User> GetUserAsync(int id);

        Task<TokenInfo> GetTokenInfo(string token);

        Task<TokenInfo> Authenticate(string email, string password);

        Task<int> AddUser(CreateUser user);
    }
}
