using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

using Moq;

using abacanet.diamond.domain.model;
using abacanet.diamond.infrastructure.persistence;

namespace abacanet.diamond.application.facade.MOCK.Tests
{
    [TestClass]
    public class UserInviteFacadeMOCKTest
    {
        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        [TestCategory("MOCK"), TestMethod]   
        [Ignore]     
        public void AddUserInviteFacadeMOCK_SUCESS()
        {
            var mockSet = new Mock<IDbSet<UserInviteDomainModel>>();
            var mockContext = new Mock<UnitOfWork>();

            var userInviteDomain = new UserInviteDomainModel();

            mockContext.Setup(m => m.UserInvite).Returns(mockSet.Object);

            var domain = new UserInviteFacade(mockContext.Object);
            domain.AddUserInvite(userInviteDomain);

            mockSet.Verify(m => m.Add(It.IsAny<UserInviteDomainModel>()), Times.Once());            
        }
    }
}
