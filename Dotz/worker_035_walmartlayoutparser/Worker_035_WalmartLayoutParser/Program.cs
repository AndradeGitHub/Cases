using System;
using System.Xml.Serialization;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using log4net;

using worker_WalmartLayoutParser.infrastructure;
using worker_WalmartLayoutParser.consumer;
using worker_WalmartLayoutParser.map;

namespace worker_WalmartLayoutParser
{
    class Program
    {                
        private static readonly string productCatalog = ConfigurationManager.AppSettings["ProductCatalog"];
        private static readonly string pathFileGeneration = ConfigurationManager.AppSettings["PathFileGeneration"];
        private static readonly string pathFileCopy = ConfigurationManager.AppSettings["PathFileCopy"];
        private static readonly string pathFileMove = ConfigurationManager.AppSettings["PathFileMove"];                    
        private static readonly string xmlFileName = ConfigurationManager.AppSettings["XMLFileName"];
        private static readonly string qtCatalogConsumer = ConfigurationManager.AppSettings["QtCatalogConsumer"]; 

        //API Walmart
        private static readonly string urlAPIWalmart = ConfigurationManager.AppSettings["UrlAPIWalmart"];
        private static readonly string clientIdAPIWalmart = ConfigurationManager.AppSettings["ClientIdAPIWalmart"];
        private static readonly string clientSecretAPIWalmart = ConfigurationManager.AppSettings["ClientSecretAPIWalmart"];
        private static readonly string grantTypeAPIWalmart = ConfigurationManager.AppSettings["GrantTypeAPIWalmart"];        
        
        static void Main(string[] args)
        {
            try
            {
                Log.RecordInfo("# INICIO DO PROCESSAMENTO");
                Log.RecordInfo("******************************************************************************");
                Log.RecordInfo("******************************************************************************");
                Log.RecordInfo(string.Empty);

                #region API WALLMART    
                var supllier = new SupplierWalmart(urlAPIWalmart);

                Log.RecordInfo("# INÍCIO - API WALLMART AUTENTICAÇÃO");                
                var retAuthenticate = supllier.Autenticar(clientIdAPIWalmart, clientSecretAPIWalmart, grantTypeAPIWalmart);
                Log.RecordInfo("# FIM - API WALLMART AUTENTICAÇÃO");

                Log.RecordInfo("# INÍCIO - API WALLMART RECEBIMENTO DE TODOS OS PRODUTOS");
                int tentativas = 0;
                var retGetCatalog = supllier.GetCatalog(retAuthenticate.AccessToken, productCatalog, Convert.ToInt32(qtCatalogConsumer), ref tentativas);
                Log.RecordInfo(string.Format("# FIM - API WALLMART RECEBIMENTO DE TODOS OS PRODUTOS / {0} TENTATIVA(s)", tentativas));
                #endregion API WALLMART                

                Log.RecordInfo("# INÍCIO - EXTRAÇÃO DO JSON DO ARQUIVO ZIP");
                FileGenerator.DescompressorZipFile(retGetCatalog, "WalmartCatalogPartial.zip");
                Log.RecordInfo("# FIM - EXTRAÇÃO DO JSON DO ARQUIVO ZIP");
                
                string currentDirName = string.Concat(Directory.GetCurrentDirectory(), "\\arquivo\\");
                string[] JsonFile = Directory.GetFiles(currentDirName, "*.json");
                string strJson = File.ReadAllText(JsonFile[0]);

                Log.RecordInfo("# INÍCIO - JSON TO OBJECT");
                var objJson = Converter.JsonToObject(strJson);
                Log.RecordInfo("# FIM - JSON TO OBJECT");

                Log.RecordInfo("# INÍCIO - MAPEAMENTO");                
                var mapping = MapFactory.CreateMap<Mapping>();
                var catalogoProduto = mapping.ParseCatalogo(objJson);
                Log.RecordInfo("# FIM - MAPEAMENTO");

                Log.RecordInfo("# INÍCIO - OBJECT TO XML");
                var objXml = Converter.ObjectToXML(catalogoProduto);
                Log.RecordInfo("# FIM - OBJECT TO XML");

                Log.RecordInfo("# INÍCIO - GERAÇÃO ARQUIVO XML");                
                FileGenerator.GenerateXmlFile(objXml, string.Concat(pathFileGeneration, xmlFileName), "xml");
                Log.RecordInfo("# FIM - GERAÇÃO ARQUIVO XML");

                string xmlFile = string.Concat(xmlFileName, ".xml");

                Log.RecordInfo("# INÍCIO - ARCHIVE MANAGER");
                //ArchiveManager.Get(pathFileGeneration, string.Empty);                 
                ArchiveManager.Copy(string.Concat(pathFileGeneration, xmlFile), string.Concat(pathFileCopy, xmlFile));
                ArchiveManager.Move(string.Concat(pathFileGeneration, xmlFile), string.Concat(pathFileMove, xmlFile));
                Log.RecordInfo("# FIM - ARCHIVE MANAGER");

                Log.RecordInfo("# INÍCIO - APAGANDO ARQUIVOS");
                FileGenerator.DeleteDirectory(currentDirName);

                string[] deleteFiles = { "WalmartCatalogPartial.zip", xmlFile };        
                FileGenerator.DeleteFile(deleteFiles);                
                Log.RecordInfo("# FIM - APAGANDO ARQUIVOS");                
            }
            catch(Exception ex)
            {
                Log.RecordError(ex);
            }
            finally
            {
                Log.RecordInfo(string.Empty);
                Log.RecordInfo("******************************************************************************");
                Log.RecordInfo("******************************************************************************");
                Log.RecordInfo("# FIM DO PROCESSAMENTO");

                Environment.Exit(1);
            }
        }
    }
}
