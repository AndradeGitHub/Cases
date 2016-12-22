using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository.interfaces;

namespace abacanet.diamond.domain.repository.Tests
{
    [TestClass]
    public class OrderRepositoryTest
    {
        private static IRepository<OrderDomainModel> _repositoryFactory;        

        [TestInitialize]
        public void InitializeTest()
        {                        
            _repositoryFactory = RepositoryFactory.CreateRepository<OrderDomainModel, OrderRepository>();            
        }

        [Ignore]
        [TestCategory("DATABASE"), TestMethod]
        public void GetAllOrders_SUCESS()
        {           
            var lstOrder = _repositoryFactory.GetAll();

            Assert.IsTrue(lstOrder.Count() > 0);
        }

        public void DeleteOrder_SUCESS()
        {               
            Assert.IsTrue(_repositoryFactory.Delete(OrderEntity()) > 0);
        }

        #region Entity
        private List<OrderDomainModel> OrderEntity()
        {
            OrderDomainModel orderEntity = new OrderDomainModel();
            orderEntity.Id = 1;
            orderEntity.OrderName = "Order Name";

            List<OrderDomainModel> lstOrderEntity = new List<OrderDomainModel>();
            lstOrderEntity.Add(orderEntity);

            return lstOrderEntity;
        }
        #endregion Entity
    }
}
