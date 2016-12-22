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
    public class UserMOCKTest
    {
        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        [TestCategory("MOCK"), TestMethod]
        public void AddUserMOCK_SUCESS()
        {
            var mockSet = new Mock<IDbSet<UserDomainModel>>();
            var mockContext = new Mock<UnitOfWork>();

            var userDomain = new UserDomainModel();

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var domain = new User(mockContext.Object);
            domain.AddUser(userDomain);

            //mockSet.Verify(m => m.Add(It.IsAny<UserDomainModel>()), Times.Once());                          
        }        
    }
}
