using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace repopractise.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> Get(int id);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        // Task<IEnumerable<TEntity>> ListAsync();
    }

}