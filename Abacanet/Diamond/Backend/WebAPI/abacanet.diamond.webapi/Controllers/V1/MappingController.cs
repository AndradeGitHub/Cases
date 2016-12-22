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
    [ApiVersion1RoutePrefix("Mapping")]
    public class MappingController : ApiController
    {
        private static readonly MappingFacade mappingFacade = new MappingFacade();     

        //GET: /api/v1/Mapping/
        [Route("GetMapping", Name = "GetMappingV1")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetMapping()
        {
            var result = mappingFacade.GelAllMapping<MappingViewModel>();

            if (result == null)
                return NotFound();

            return Ok(result);
        }    
    }
}