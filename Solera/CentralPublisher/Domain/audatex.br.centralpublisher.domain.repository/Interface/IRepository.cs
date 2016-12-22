using System;
using System.Collections.Generic;
using System.Linq;

namespace audatex.br.centralpublisher.domain.repository.interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        
        int Delete(T entity);

        IEnumerable<T> GetAll();

        T GetById(int id);
    }
}
