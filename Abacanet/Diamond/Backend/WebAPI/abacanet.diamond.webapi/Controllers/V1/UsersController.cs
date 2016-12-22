using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

using abacanet.diamond.webapi.Models;
using abacanet.diamond.webapi.common;
using abacanet.diamond.webapi.common.Routing;
using abacanet.diamond.application;

namespace abacanet.diamond.webapi.Controllers.V1
{
    [ApiVersion1RoutePrefix("Users")]
    public class UsersController : ApiController
    {
        private static readonly UserFacade userFacade = new UserFacade();

        //GET: /api/v1/Users/paged?pageNumber=1&pageSize=10&sortBy=Name&order=ASC
        [Route("paged", Name = "GetUsersPaged")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetUsersPaged(int pageNumber, int pageSize, string sortBy = "Name", string order = "ASC")
        {
            var result = userFacade.GelAllUsers<UserViewModel>();

            var totalPages = (int) Math.Ceiling((double)result.Count/pageSize);
            var pagedEntity = new GenericEntityPagedList<UserViewModel>()
            {
                ActualPage = pageNumber,
                Count = result.Count,
                Pages = totalPages,
                Entity = result.Skip(pageSize * (pageNumber - 1)).Take(pageSize)
            };

            return Ok(pagedEntity);
        }

        //Todo: REMOVE AFTER IMPLEMENT PAGED ON THE FRONTEND
        //GET: /api/v1/Users/
        //[Route("", Name = "GetUsers")]
        //[HttpGet]
        //public IHttpActionResult GetUsers()
        //{
        //    return Ok(_listOfUsers);
        //}

        //GET: /api/v1/Users/
        [Route("{login, password}", Name = "LoginV1")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult Login(string login, string password)
        {                
            var result = userFacade.Login<UserViewModel>(login, password);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //POST: /api/v1/Users/
        [Route("AddUser", Name = "AddUserV1")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult AddUser(UserViewModel userView)
        {            
            userFacade.AddUser(userView);

            return Ok(HttpStatusCode.OK);
        }

        //POST: /api/v1/Users/1
        [Route("{id}", Name = "DeleteUserV1")]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteUser(int id)
        {            
            userFacade.DeleteUser(id);

            return Ok(HttpStatusCode.OK);
        }

        //GET: /api/v1/Users/
        [Route("GetUsersAuthenticate", Name = "GetUsersAuthenticateV1")]
        [HttpGet]
        [Authorize]
        public string GetUsersAuthenticate()
        {            
            return "Test Users Authenticate OK.";
        }
    }
}