using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbSet<T> Set<T>() where T : class;

        Task<int> SaveAsync();
     
    }
}
