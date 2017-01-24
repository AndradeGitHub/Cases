using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using System.Net.Http;
using System.Web.Http;

using audatex.br.audabridge2.application;

namespace audatex.br.audabridge2.service.webapi.Controllers
{    
    [RoutePrefix("api/Bradesco")]
    public class BradescoController : ApiController
    {
        private const string seguradora = "Bradesco";

        private static readonly T1Facade t1Facade = new T1Facade();
     
        // POST api/Bradesco/T1Integrate 
        [Route("T1Integrate")]
        [HttpPost]
        //[Authorize]
        //public IHttpActionResult Post(TestModel bradescoObj)
        public IHttpActionResult T1Integrate(object bradescoObj)
        {                     
            t1Facade.T1Integrate(bradescoObj, seguradora);            

            return Ok(HttpStatusCode.OK);
        }
    }

    //[DataContract(Namespace = "")]
    //public class TestModel 
    //{
    //    [DataMember]
    //    public string Output { get; set;  }

    //}
}
