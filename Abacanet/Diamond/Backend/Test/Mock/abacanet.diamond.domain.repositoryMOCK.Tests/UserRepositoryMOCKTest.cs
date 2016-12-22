using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using abacanet.diamond.domain.model;
using abacanet.diamond.infrastructure.persistence;
using abacanet.diamond.domain.repository;

namespace abacanet.diamond.domain.repositoryMOCK.Tests
{
    [TestClass]
    public class UserRepositoryMOCKTest
    {
        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        [TestCategory("MOCK"), TestMethod]
        public void AddUserRepositoryMOCK_SUCESS()
        {
            var mockSet = new Mock<IDbSet<UserDomainModel>>();
            var mockContext = new Mock<UnitOfWork>();

            var userDomain = new UserDomainModel();

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var repository = new UserRepository(mockContext.Object);
            repository.Add(userDomain);

            mockSet.Verify(m => m.Add(It.IsAny<UserDomainModel>()), Times.Once());            
        }
    }
}
