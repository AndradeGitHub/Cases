using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.domain.interfaces;
using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository;
using abacanet.diamond.infrastructure.persistence;

namespace abacanet.diamond.domain.Tests
{
    [TestClass]
    public class UserTest
    {   
        [TestCategory("DATABASE"), TestMethod]
        public void AddUser_SUCESS()
        {
            var unitOfWork = new UnitOfWork();
            var user = new User(unitOfWork);

            UserDomainModel userDomain = new UserDomainModel();            
            userDomain.Login = "Login1";
            userDomain.Password = "Teste1";
            userDomain.FirstName = "First Name";
            userDomain.LastName = "Last Name";
            userDomain.Status = 1;

            user.AddUser(userDomain);

            //unitOfWork.Commit();
        }
    }
}
