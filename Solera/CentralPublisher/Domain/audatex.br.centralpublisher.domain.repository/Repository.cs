using System;
using System.Collections.Generic;
using System.Linq;

using audatex.br.centralpublisher.domain.repository.interfaces;

namespace audatex.br.centralpublisher.domain.repository
{
    public class Repository<T> : IRepository<T>
    {
        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual int Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(int id)
        {
            throw new NotImplementedException();
        }

 
    }
}