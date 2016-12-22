using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using abacanet.diamond.domain.model;
using abacanet.diamond.infrastructure.persistence;

namespace abacanet.diamond.domain.MOCK.Tests
{
    [TestClass]
    public class UserInviteMOCKTest
    {
        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        [TestCategory("MOCK"), TestMethod]
        public void AddUserInviteMOCK_SUCESS()
        {
            var mockSet = new Mock<IDbSet<UserInviteDomainModel>>();
            var mockContext = new Mock<UnitOfWork>();

            var userInviteDomain = new UserInviteDomainModel();

            mockContext.Setup(m => m.UserInvite).Returns(mockSet.Object);

            var domain = new UserInvite(mockContext.Object);
            domain.AddInviteUser(userInviteDomain);

            //mockSet.Verify(m => m.Add(It.IsAny<UserInviteDomainModel>()), Times.Once());            
        }
    }
}
