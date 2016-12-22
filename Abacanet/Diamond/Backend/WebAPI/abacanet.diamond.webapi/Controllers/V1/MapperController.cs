using abacanet.diamond.application;
using abacanet.diamond.webapi.common.Routing;
using abacanet.diamond.webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace abacanet.diamond.webapi.Controllers.V1
{
    [ApiVersion1RoutePrefix("Mapper")]
    public class MapperController : ApiController
    {
        // GET: /api/v1/Mapper
        [Route("Index", Name = "GetV1")]
        [HttpGet]
        public IEnumerable<IEnumerable<MapperResponseModel>> Index()
        {
            var mapper = new MapperFacade();
            return mapper.Get<MapperResponseModel>();
        }
    }
}
