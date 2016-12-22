using System.Configuration;
using System.Web.Http;

using abacanet.diamond.application;
using abacanet.diamond.webapi.common.Routing;
using abacanet.diamond.webapi.Models;

namespace abacanet.diamond.webapi.Controllers.V1
{
    [ApiVersion1RoutePrefix("Invites")]
    public class InvitesController : ApiController
    {
        private static readonly string mailUrl = ConfigurationManager.AppSettings["MailUrl"];
        private static readonly UserInviteFacade userInviteFacade = new UserInviteFacade();

        //POST: /api/v1/Invites/
        [Route("UserInvite", Name = "InviteUserV1")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult UserInvite(UserInviteViewModel userInviteView)
        {            
            userInviteView.Url = mailUrl;
            
            var result = userInviteFacade.AddUserInvite(userInviteView);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //GET: /api/v1/Invites/
        [Route("UserInvite/{id}", Name = "GetUserInviteV1")]
        [HttpGet]
        public IHttpActionResult GetUserInvite(int id)
        {
            var result = userInviteFacade.GetUserInvite<UserInviteViewModel>(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //GET: /api/v1/Invites/
        [Route("GetInvitesAuthenticate", Name = "GetInvitesAuthenticateV1")]
        [HttpGet]
        [Authorize]
        public string GetInvitesAuthenticate()
        {
            return "Test Invites Authenticate OK.";
        }
    }
}