using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Diagnostics;
using System.Linq;

using abacanet.diamond.webapi.Models;
using abacanet.diamond.webapi.common;
using abacanet.diamond.webapi.common.Routing;
using abacanet.diamond.application;

namespace abacanet.diamond.webapi.Controllers.V1
{
    [ApiVersion1RoutePrefix("ProfitAndLoss")]
    public class ProfitAndLossController : ApiController
    {
        private static readonly ProfitAndLossFacade profitAndLossFacade = new ProfitAndLossFacade();

        //POST: /api/v1/ProfitAndLoss/UploadFile
        [Route("UploadFile", Name = "UploadFileV1")]
        [HttpPost]
        [Authorize]        
        public async Task<IHttpActionResult> UploadFile()
        {          
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/FilesUpload");                     

            if (!Request.Content.IsMimeMultipartContent("form-data"))            
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);            

            Utils.CreateDirectory(fileSaveLocation);
            
            var provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);                        

            await Request.Content.ReadAsMultipartAsync(provider);

            string[] extensionsExcel = { ".xlsx", ".xls", ".csv" };              
            if (!extensionsExcel.Contains(Path.GetExtension(provider.FileData[0].LocalFileName)))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            profitAndLossFacade.UploadFile(provider.FileData[0].LocalFileName);

            return Ok(HttpStatusCode.OK);            
        }
    }
}