using System;
using System.Collections.Generic;
using System.Linq;

using abacanet.diamond.domain.repository.interfaces;

namespace abacanet.diamond.domain.repository
{
    public class Repository<T> : IRepository<T>
    {
        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual int Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(T entity)
        {
            throw new NotImplementedException();
        }        

        public virtual Tuple<IQueryable<T>, int> GetAllPaged(int pageNumber, int pageSize, string orderField, string ordering)
        {
            throw new NotImplementedException();
        }
    }
}
