using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.domain.Interface;
using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository;

namespace abacanet.diamond.domain.Tests
{
    [TestClass]
    public class OrderTest
    {
        private static IOperation<OrderDomainModel> _domainFactory;        

        [TestInitialize]
        public void InitializeTest()
        {            
            _domainFactory = DomainFactory.CreateDomain<OrderDomainModel, Order>();            
        }

        #region DATABASE
        [Ignore]
        [TestCategory("DATABASE"), TestMethod]
        public void GetOrdersToCount_SUCESS()
        {
            OrderDomainModel orderEntity = new OrderDomainModel();
            orderEntity.OrderName = "Order Name";

            Assert.IsNotNull(_domainFactory.GetOrdersToCount(orderEntity));
        }

        [Ignore]
        [TestCategory("DATABASE"), TestMethod]
        public void DeleteOrder_SUCESS()
        {            
            Assert.IsTrue(_domainFactory.DeleteOrder(OrderEntity()) > 0);
        }                
        #endregion DATABASE

        #region Entity
        private List<OrderDomainModel> OrderEntity()
        {
            OrderDomainModel orderEntity = new OrderDomainModel();            
            orderEntity.OrderName = "Order Name";

            List<OrderDomainModel> lstOrderEntity = new List<OrderDomainModel>();
            lstOrderEntity.Add(orderEntity);            

            return lstOrderEntity;
        }
        #endregion Entity
    }
}
