using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Newtonsoft.Json;

using worker_WalmartLayoutParser.catalogoProdutoWalmart;
using worker_WalmartLayoutParser.xds_952;

namespace worker_WalmartLayoutParser.infrastructure
{
    public class Converter
    {
        public static RootObject JsonToObject(string strJson)
        {
            RootObject objJson = JsonConvert.DeserializeObject<RootObject>(strJson);            

            return objJson;
        }

        public static XDocument ObjectToXML(Catalogo objCatalog)
        {
            string strCatalog = JsonConvert.SerializeObject(objCatalog, Formatting.Indented);

            XDocument docXML = JsonConvert.DeserializeXNode(strCatalog, "CATALOGO", false);            

            return docXML;
        }
    }
}
