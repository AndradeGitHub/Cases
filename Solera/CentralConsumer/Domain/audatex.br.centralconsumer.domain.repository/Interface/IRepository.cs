using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audatex.br.centralconsumer.domain.repository.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);

        int Delete(T entity);

        IEnumerable<T> GetAll();

        T GetById(int id);
    }
}
