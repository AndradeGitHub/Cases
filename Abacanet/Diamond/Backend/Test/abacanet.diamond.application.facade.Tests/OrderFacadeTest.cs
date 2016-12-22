using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.application;
using abacanet.diamond.webapi.Models;

namespace abacanet.diamond.application.facade.Tests
{
    [TestClass]
    public class OrderTest
    {
        private static OrderFacade _orderFacade;        

        [TestInitialize]
        public void InitializeTest()
        {
            _orderFacade = new OrderFacade();
        }

        #region Entity
        [TestCategory("DATABASE"), TestMethod]
        [Ignore]
        public void GetOrdersToCountFACADE_SUCESS()
        {
            OrderViewModel orderEntity = new OrderViewModel();
            orderEntity.OrderName = "Order Name";
            
            var lstOrderViewModel = _orderFacade.GetOrdersToX<OrderViewModel>(orderEntity);

            Assert.IsNotNull(lstOrderViewModel);
        }
        #endregion DATABASE
    }
}
