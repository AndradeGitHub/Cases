using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.application;
using abacanet.diamond.webapi.Models;

namespace abacanet.diamond.application.facade.Tests
{
    [TestClass]
    public class MappingFacadeTest
    {
        [TestCategory("DATABASE"), TestMethod]
        public void GelAllMappingFacade_SUCESS()
        {
            var mappingFacade = new MappingFacade();

            var ret = mappingFacade.GelAllMapping<MappingViewModel>();

            Assert.IsNotNull(ret);
        }
    }
}
