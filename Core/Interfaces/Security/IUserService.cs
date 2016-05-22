using Core.DatabaseModels.Security;
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
    }
}
