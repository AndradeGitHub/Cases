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
    public class UserInviteRepositoryMOCKTest
    {
        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        [TestCategory("MOCK"), TestMethod]
        public void AddInviteUserRepositoryMOCK_SUCESS()
        {
            var mockSet = new Mock<IDbSet<UserInviteDomainModel>>();
            var mockContext = new Mock<UnitOfWork>();

            var userInviteDomain = new UserInviteDomainModel();

            mockContext.Setup(m => m.UserInvite).Returns(mockSet.Object);

            var repository = new UserInviteRepository(mockContext.Object);
            repository.Add(userInviteDomain);

            mockSet.Verify(m => m.Add(It.IsAny<UserInviteDomainModel>()), Times.Once());            
        }
    }
}
