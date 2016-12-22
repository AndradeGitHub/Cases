using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using abacanet.diamond.domain.interfaces;

namespace abacanet.diamond.domain
{
    public class Operation<T> : IOperation<T>
    {
        public virtual bool EmailInviteUser(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void AddInviteUser(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void AddUser(T entity)
        {
            throw new NotImplementedException();
        }      

        public virtual List<T> GetOrdersToCount(T entity)
        {
            throw new NotImplementedException();
        }     

        public virtual int DeleteOrder(List<T> entity)
        {
            throw new NotImplementedException();
        }        
    }
}
