using System.Collections.Generic;
using System.Threading.Tasks;

namespace abacanet.diamond.domain.interfaces
{
    public interface IOperation<T>
    {
        void AddInviteUser(T entity);
        bool EmailInviteUser(T entity);
        void AddUser(T entity);
        List<T> GetOrdersToCount(T entity);        
        int DeleteOrder(List<T> entity);
    }    
}