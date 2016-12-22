using System;
using System.Collections.Generic;
using System.Linq;

namespace abacanet.diamond.domain.repository.interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);          
        int Delete(T entity);
        List<T> GetAll();
        T GetById(int id);
        T Get(T entity);
        Tuple<IQueryable<T>, int> GetAllPaged(int pageNumber, int pageSize, string orderField, string ordering);
    }
}
