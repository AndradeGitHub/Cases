using System.Web.Http;

using abacanet.diamond.webapi.common.Routing;

namespace abacanet.diamond.webapi.Controllers.V1
{
    [ApiVersion1RoutePrefix("VersioningTest")]
    public class VersioningTestController : ApiController
    {
        //GET: /api/versiontest/
        [Route("", Name = "GetStringTest")]
        [HttpGet]
        public string GetStringTest()
        {
            return "This is version 1";
        }
    }
}