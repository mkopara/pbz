using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAsyncDbSet<T> : IDbSet<T> where T : class
    {
        Task<T> FindAsync(params Object[] keyValues);
    }
}
