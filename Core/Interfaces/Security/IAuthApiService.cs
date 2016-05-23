using Core.DatabaseModels.Security;
using Core.DomainModels.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Security
{
    /// <summary>
    /// Interface for validation via web requests
    /// </summary>
    public interface IAuthApiService
    {
        Task<TokenInfo> ValidateToken(string token);

        Task<TokenInfo> Authenticate(string email, string password);

    }
}
