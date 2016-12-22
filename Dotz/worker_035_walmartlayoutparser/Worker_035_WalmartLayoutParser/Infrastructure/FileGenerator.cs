using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;

using System.Xml.Linq;

using Newtonsoft.Json;

namespace worker_WalmartLayoutParser.infrastructure
{
    public class FileGenerator
    {
        public static void GenerateXmlFile(XDocument objXml, string pathFile, string extensionFile)
        {
            //Deleta Xml existente
            bool xmlFile = File.Exists(string.Concat(pathFile, ".", extensionFile));
            if (xmlFile)
                File.Delete(string.Concat(pathFile, ".", extensionFile));

            //var declaration = new XDeclaration("1.0", "iso-8859-1", "");
            var declaration = new XDeclaration("1.0", "utf-8", "yes");
            objXml.Declaration = declaration;

            objXml.Save(string.Concat(pathFile, ".", extensionFile));            
        }    

        public static void DescompressorZipFile(byte[] byteFile, string fileName)
        {
            //Deleta zip existente
            bool xmlFile = File.Exists(fileName);
            if (xmlFile)
                File.Delete(fileName);            

            if (Directory.Exists(string.Concat(Directory.GetCurrentDirectory(), "\\arquivo\\")))
                Directory.Delete(string.Concat(Directory.GetCurrentDirectory(), "\\arquivo\\"), true);
            else
                Directory.CreateDirectory(string.Concat(Directory.GetCurrentDirectory(), "\\arquivo\\"));

            File.WriteAllBytes(fileName, byteFile);

            ZipFile.ExtractToDirectory(fileName, string.Concat(Directory.GetCurrentDirectory(), "\\arquivo\\"));
        }

        public static void DeleteDirectory(string directoryName)
        {
            if (Directory.Exists(directoryName))
                Directory.Delete(directoryName, true);
        }

        public static void DeleteFile(string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
            }
        }
    }
}
