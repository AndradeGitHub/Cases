using System;
using System.Configuration;
using System.Linq;

using audatex.br.audabridge2.application.adapter.entrada.bradesco;
using audatex.br.audabridge2.application.adapter.saida.bradesco;
using audatex.br.audabridge2.domain.model;
using audatex.br.audabridge2.domain.model.i360.input;
using audatex.br.audabridge2.domain.model.bradesco;
using audatex.br.audabridge2.domain.repository;
using audatex.br.audabridge2.infrastructure.mef;
using audatex.br.audabridge2.infrastructure.common;
using audatex.br.audabridge2.infrastructure.persistence;

namespace audatex.br.audabridge2.application.common
{
    public class Util
    {
        private readonly string _path = "C:\\WorkspaceBR\\AudaBridge 2.0\\Main\\Backend\\Plugins";

        private readonly string _mongoDbConnectionString = ConfigurationManager.ConnectionStrings["MongoDbConnectionString"].ConnectionString;
        private readonly string _mongoDbDatabaseName = ConfigurationManager.AppSettings["MongoDbDatabaseName"];

        private static dynamic _repositoryBase;
        private static dynamic _repositoryIntegracaoException;

        private static dynamic _unitOfWork;
        private static dynamic _repositoryPlugin;
        private static dynamic _repositoryIntegracao;

        private static dynamic _t1AdapterBradesco;
        private static dynamic _t3AdapterBradesco;

        public Util(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryPlugin = RepositoryFactory.CreateRepositoryCustom<PluginModel, PluginRepository>(_unitOfWork);
            _repositoryIntegracao = RepositoryFactory.CreateRepository<IntegracaoModel, Repository<IntegracaoModel>>(_unitOfWork);

            //NoSql
            _repositoryBase = new RepositoryBase(_mongoDbConnectionString, _mongoDbDatabaseName);
            _repositoryIntegracaoException = RepositoryFactory.CreateRepository<IntegracaoExceptionModel, Repository<IntegracaoExceptionModel>>(_repositoryBase.CreateDataBase(), "IntegracaoException");

            _t1AdapterBradesco = new T1BradescoAdapter();
            _t3AdapterBradesco = new T3BradescoAdapter();
        }

        public InputModel AdapterEntrada(object objT1Json, string seguradora)
        {
            var objI360 = new InputModel();

            switch (seguradora.ToLower())
            {
                case "bradesco":
                    var objT1 = Converter.JsonToObject<IntegracaoEntradaCommon>(objT1Json.ToString());

                    objI360 = _t1AdapterBradesco.ConvertBradescoToI360(objT1);
                    break;
            }

            return objI360;
        }

        public object AdapterSaida(object objT3, string seguradora)
        {
            var objSeguradora = new object();

            switch (seguradora.ToLower())
            {
                case "bradesco":
                    objSeguradora = _t3AdapterBradesco.ConvertI360ToBradesco(objT3);
                    break;
            }

            return objSeguradora;
        }

        public void ExecutePlugin(int tomada, object i360Obj, string seguradora)
        {
            PluginModel pluginModel = new PluginModel();
            pluginModel.Tomada = tomada;
            pluginModel.Seguradora = new SeguradoraModel();
            pluginModel.Seguradora.Nome = seguradora;

            //Recupera o plugin a ser executado
            var plugin = _repositoryPlugin.GetPlugin(pluginModel);

            //Executa o plugin solicitado
            PluginFactory.CreatePlugin(_path, plugin.Nome, i360Obj);
        }

        public void SaveException(int idIntegracao, string innerException)
        {
            IntegracaoExceptionModel integracaoEx = new IntegracaoExceptionModel();
            integracaoEx.IdIntegracao = idIntegracao;
            integracaoEx.Exception = innerException;

            _repositoryIntegracaoException.AddAsync(integracaoEx);
        }
    }
}
