using System;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository;
using abacanet.diamond.domain.repository.interfaces;
using abacanet.diamond.infrastructure.persistence;

namespace abacanet.diamond.domain.repository.Tests
{
    [TestClass]
    public class MappingRepositoryTest
    {
        [TestCategory("DATABASE"), TestMethod]
        public void GetAllMappingRepository_SUCESS()
        {
            var unitOfWork = new UnitOfWork();
            var mappingRepository = new MappingRepository(unitOfWork);

            var lstUser = mappingRepository.GetAll();

            Assert.IsNotNull(lstUser);
        }
    }
}
