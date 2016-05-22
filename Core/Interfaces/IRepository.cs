using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    //Generic repository
    public interface IRepository<TEntity> where TEntity : class
    {

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        Task<TEntity> GetByIDAsync(object id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);

    }
}
