using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository.interfaces;
using abacanet.diamond.infrastructure.persistence;

namespace abacanet.diamond.domain.repository.Tests
{
    [TestClass]
    public class UserInviteRepositoryTest
    {
        private static UnitOfWork _unitOfWork;
        private static IRepository<UserInviteDomainModel> _repositoryFactory;

        [TestInitialize]
        public void InitializeTest()
        {
            _unitOfWork = new UnitOfWork();
            _repositoryFactory = RepositoryFactory.CreateRepository<UserInviteDomainModel, UserInviteRepository>(_unitOfWork);
        }
        
        [TestCategory("DATABASE"), TestMethod]        
        public void AddInviteUserRepository_SUCESS()
        {
            _repositoryFactory.Add(UserInviteDomain());            
        }

        #region Entity
        private UserInviteDomainModel UserInviteDomain()
        {
            UserInviteDomainModel UserInvite = new UserInviteDomainModel();
            UserInvite.FirstName = "First Name Teste";
            UserInvite.LastName = "Last Name";
            UserInvite.RequestDate = DateTime.Now;

            return UserInvite;
        }
        #endregion Entity
    }
}
