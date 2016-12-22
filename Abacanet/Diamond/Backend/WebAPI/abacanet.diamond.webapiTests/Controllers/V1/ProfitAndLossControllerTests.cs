using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net.Http.Headers;

using System.Net.Http;

namespace abacanet.diamond.webapi.Controllers.V1.Tests
{
    [TestClass()]
    public class ProfitAndLossControllerTests
    {
        [TestMethod()]
        public void UploadTest()
        {
            const string fileName = @"C:\Users\Douglas\Desktop\HelloWorld.txt";

            using (var client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            using (var fileStream = File.OpenRead(fileName))
            {

                var fileContent = new StreamContent(fileStream);

                fileContent.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("text/plain");

                fileContent.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("form-data")
                    {
                        FileName = fileName
                    };

                content.Add(fileContent);
                var result = client.PostAsync("http://localhost:52329/api/V1/ProfitAndLoss/Upload", content).Result;
                result.EnsureSuccessStatusCode();
            }
        }
    }
}