using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Newtonsoft.Json;
using System.Xml;

namespace audatex.br.centralpublisher.infrastructure.common
{
    public class Converter
    {
        public static dynamic XmlToJson(string strXML)
        {
            XmlDocument ObjXML = new XmlDocument();
            ObjXML.LoadXml(strXML);
 
            return JsonConvert.DeserializeObject(JsonConvert.SerializeXmlNode(ObjXML));
        }

        public static string ObjToJson(dynamic obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static XDocument ObjToXML(dynamic Obj, string Root)
        {
            string json = JsonConvert.SerializeObject(Obj, Newtonsoft.Json.Formatting.None);

            XDocument ObjXML = JsonConvert.DeserializeXNode(json, Root);

            return ObjXML;
        }
   
    }
}
