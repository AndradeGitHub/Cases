using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.domain.interfaces;
using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository;
using abacanet.diamond.infrastructure.persistence;

namespace abacanet.diamond.domain.Tests
{
    [TestClass]
    public class UserInviteTest
    {
        [TestCategory("DATABASE"), TestMethod]
        public void AddUserInvite_SUCESS()
        {
            var unitOfWork = new UnitOfWork();
            var userInvite = new UserInvite(unitOfWork);

            userInvite.AddInviteUser(UserInviteDomain());            
        }

        #region Entity
        private List<UserInviteDomainModel> UserInviteDomainCol()
        {
            UserInviteDomainModel UserInviteDomain = new UserInviteDomainModel();
            //UserInviteDomain.Id = 1;
            //UserInviteDomain.UserName = "User Name";

            List<UserInviteDomainModel> lstUserInviteDomain = new List<UserInviteDomainModel>();
            lstUserInviteDomain.Add(UserInviteDomain);

            return lstUserInviteDomain;
        }
        private UserInviteDomainModel UserInviteDomain()
        {
            UserInviteDomainModel UserInviteDomain = new UserInviteDomainModel();
            UserInviteDomain.FirstName = "First Name";
            UserInviteDomain.LastName = "First Name";
            UserInviteDomain.Company = "Company";
            UserInviteDomain.Email = "user@email.com";
            UserInviteDomain.Notes = "Notes";
            //UserInviteDomain.RequestDate = DateTime.Now;
            UserInviteDomain.Address = "Address";
            UserInviteDomain.City = "City";
            UserInviteDomain.State = "State";
            UserInviteDomain.ZipCode = "55";

            return UserInviteDomain;
        }
        #endregion Entity
    }
}
