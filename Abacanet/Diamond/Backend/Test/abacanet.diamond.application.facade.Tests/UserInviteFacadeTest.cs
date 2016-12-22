using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.application;
using abacanet.diamond.webapi.Models;

namespace abacanet.diamond.application.facade.Tests
{
    [TestClass]
    public class UserInviteFacadeTest
    {       
        [TestCategory("DATABASE"), TestMethod]
        [Ignore]
        public void InviteUserFacade_SUCESS()
        {
            var userInviteFacade = new UserInviteFacade();

            var ret = userInviteFacade.AddUserInvite(UserInviteView());

            Assert.IsNotNull(ret);
        }

        #region Entity
        private UserInviteViewModel UserInviteView()
        {
            UserInviteViewModel UserInviteView = new UserInviteViewModel();
            UserInviteView.FirstName = "First Name Test1";
            UserInviteView.LastName = "Last Name Test1";
            UserInviteView.Company = "Company Test1";
            UserInviteView.Email = "andradeit@gmail.com";                        
            UserInviteView.Address = "Dep. Emilio Carlos1";
            UserInviteView.City = "São Paulo1";
            UserInviteView.State = "SP";
            UserInviteView.ZipCode = "55";

            return UserInviteView;
        }
        #endregion Entity  
    }
}
