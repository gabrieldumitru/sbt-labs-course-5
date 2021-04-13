using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sbt_labs_course_5.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        void Create(TEntity entity);

        void CreateRange(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(Guid id);

        IQueryable<TEntity> GetAll();

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

        Task<bool> SaveAsync();
    }
}
