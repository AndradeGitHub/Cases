using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

using Moq;

using abacanet.diamond.domain.model;
using abacanet.diamond.infrastructure.persistence;

namespace abacanet.diamond.application.facade.MOCK.Tests
{
    [TestClass]
    public class UserFacadeMOCKTest
    {
        public void FixEfProviderServicesProblem()
        {
            var instance = SqlProviderServices.Instance;
        }

        [TestCategory("MOCK"), TestMethod]        
        public void AddUserFacadeMOCK_SUCESS()
        {
            var mockSet = new Mock<IDbSet<UserDomainModel>>();
            var mockContext = new Mock<UnitOfWork>();

            var userInviteDomain = new UserDomainModel();

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var domain = new UserFacade(mockContext.Object);
            domain.AddUser(userInviteDomain);                      
        }
    }
}
