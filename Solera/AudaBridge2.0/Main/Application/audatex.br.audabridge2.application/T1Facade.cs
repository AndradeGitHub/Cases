using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;

using audatex.br.audabridge2.application.common;
using audatex.br.audabridge2.domain.model;
using audatex.br.audabridge2.domain.model.enumerator;
using audatex.br.audabridge2.infrastructure.exception;
using audatex.br.audabridge2.infrastructure.persistence;

namespace audatex.br.audabridge2.application
{
    public class T1Facade
    {       
        private static dynamic _util;

        private static dynamic _unitOfWork;        
        private static dynamic _repositoryIntegracao;
        
        public T1Facade()
        {            
            _unitOfWork = new UnitOfWork();
            _repositoryIntegracao = RepositoryFactory.CreateRepository<IntegracaoModel, Repository<IntegracaoModel>>(_unitOfWork);

            _util = new Util(_unitOfWork);                                  
        }

        public void T1Integrate(object objT1, string seguradora)
        {
            var integracaoModel = new IntegracaoModel();

            try
            {           
                //Converte o obj da Seguradora para o obj do I360
                var i360Obj = _util.AdapterEntrada(objT1, seguradora);

                //OPERAÇÃO: Recebido Objeto Seguradora
                //integracaoModel.Operacao = OPERAÇÃO
                _repositoryIntegracao.Add(integracaoModel);

                //Executa o plugin de T1 para a Seguradora específica
                _util.ExecutePlugin((int)EnumTomada.Tomada1 ,i360Obj, seguradora);

                _unitOfWork.Commit();                
            }
            catch (DbEntityValidationException ex)
            {
                var entityError = EntityValidationException.Validate(ex);
                _util.SaveException(integracaoModel.Id, entityError.Item2.ToString());   
                         
                throw new DbEntityValidationException(entityError.Item1, entityError.Item2);
            }
            catch (Exception ex)
            {
                _util.SaveException(integracaoModel.Id, ex.InnerException.ToString());
                
                throw new IntegracaoException(integracaoModel.Id, ex.InnerException);
            }            
        }
    }
}
