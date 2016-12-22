using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abacanet.diamond.application;
using abacanet.diamond.webapi.Models;

namespace abacanet.diamond.application.facade.Tests
{
    [TestClass]
    public class UserFacadeTest
    {    
        [TestCategory("DATABASE"), TestMethod]
        public void GelAllUsersFacade_SUCESS()
        {
            var userFacade = new UserFacade();

            var ret = userFacade.GelAllUsers<UserViewModel>();

            Assert.IsNotNull(ret);
        }

        [TestCategory("DATABASE"), TestMethod]
        public void LoginFacade_SUCESS()
        {
            var userFacade = new UserFacade();

            var ret = userFacade.Login<UserViewModel>("Login1Teste", "Password1Teste");            
        }

        [TestCategory("DATABASE"), TestMethod]
        public void AddUserFacade_SUCESS()
        {
            var userFacade = new UserFacade();

            UserViewModel userView = new UserViewModel();
            //userDomain.RequestDate = DateTime.Now;
            userView.Email = "testeemail@teste.com.br";
            userView.Login = "Login1";
            userView.Password = "Teste1";
            userView.FirstName = "First Name";
            userView.LastName = "Last Name";
            userView.Status = 1;

            userFacade.AddUser(userView);          
        }

        [TestCategory("DATABASE"), TestMethod]
        public void DeleteFacade_SUCESS()
        {
            var userFacade = new UserFacade();

            var ret = userFacade.DeleteUser(5);

            Assert.IsTrue(ret);
        }
    }
}
