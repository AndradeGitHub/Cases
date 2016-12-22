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
    public class UserRepositoryTest
    {       
        [TestCategory("DATABASE"), TestMethod]        
        public void AddUserRepository_SUCESS()
        {
            var unitOfWork = new UnitOfWork();
            var userRepository = new UserRepository(unitOfWork);

            UserDomainModel userDomain = new UserDomainModel();
            userDomain.RequestDate = DateTime.Now;
            userDomain.Login = "Login1";
            userDomain.Password = "Teste1";
            userDomain.FirstName = "First Name";
            userDomain.LastName = "Last Name";
            userDomain.Status = 1;

            userRepository.Add(userDomain);

            //unitOfWork.Commit();
        }

        [TestCategory("DATABASE"), TestMethod]
        public void GetAllUsersRepository_SUCESS()
        {
            var unitOfWork = new UnitOfWork();
            var userRepository = new UserRepository(unitOfWork);            

            var lstUser = userRepository.GetAll();

            Assert.IsNotNull(lstUser);
        }
        
        [TestCategory("DATABASE"), TestMethod]
        public void LoginRepository_SUCESS()
        {
            var unitOfWork = new UnitOfWork();
            var userRepository = new UserRepository(unitOfWork);            

            UserDomainModel UserDomain = new UserDomainModel();
            UserDomain.Login = "Login";
            UserDomain.Password = "Teste";

            var lstUser = userRepository.Get(UserDomain);

            Assert.IsNotNull(lstUser);
        }
    }
}
