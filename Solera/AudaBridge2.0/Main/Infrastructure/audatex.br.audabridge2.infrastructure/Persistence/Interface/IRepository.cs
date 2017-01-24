using System.Threading.Tasks;
using System.Collections.Generic;

namespace audatex.br.audabridge2.infrastructure.persistence.interfaces
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);

        void AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        IEnumerable<TEntity> GetAllAsync();
        TEntity GetByIdAsync(int id);
    }
}