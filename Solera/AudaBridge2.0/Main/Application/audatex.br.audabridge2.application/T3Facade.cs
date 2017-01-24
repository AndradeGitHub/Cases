using System;
using System.Collections.Generic;
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
    public class T3Facade
    {
        private static dynamic _util;

        private static dynamic _unitOfWork;
        private static dynamic _repositoryIntegracao;

        public T3Facade()
        {
            _unitOfWork = new UnitOfWork();
            _repositoryIntegracao = RepositoryFactory.CreateRepository<IntegracaoModel, Repository<IntegracaoModel>>(_unitOfWork);

            _util = new Util(_unitOfWork);
        }

        public void T3Integrate(object objT3, string seguradora)
        {
            IntegracaoModel integracaoModel = new IntegracaoModel();

            try
            {
                //SeguradoraModel seguradoraModel = new SeguradoraModel();
                //seguradoraModel.Cnpj = "CNPJ1";
                //seguradoraModel.Nome = "Nome1";
                //seguradoraModel.Status = (int)EnumStatus.Ativo;

                //_repository.Add(seguradoraModel);
                //_unitOfWork.Commit();

                //var resultSeguradora = _repositorySeguradora.GetSeguradora();

                //List<SeguradoraModel> resultSeguradoraGetAll = _repository.GetAll();

                //var resultSeguradoraGetAll1 = resultSeguradoraGetAll.Where(x => x.Status == (int)EnumStatus.Ativo).ToList();

                //object resultSeguradoraById;
                //if (resultSeguradoraGetAll != null)
                //    resultSeguradoraById = _repository.GetById(resultSeguradoraGetAll[0].Id);

                //_repository.Delete(resultSeguradoraGetAll[resultSeguradoraGetAll.Count - 1]);
                //_unitOfWork.Commit();

                //var resultException = _repositoryIntegracaoException.GetAllAsync();

                //object resultExceptionById;
                //if (resultException != null)
                //    resultExceptionById = _repositoryIntegracaoException.GetByIdAsync(resultException[0].Id);

                //_repositoryIntegracaoException.DeleteAsync(resultException[resultException.Count - 1]); 

                //Executa o plugin de T3 para a Seguradora específica
                _util.ExecutePlugin((int)EnumTomada.Tomada3, objT3, seguradora);

                //Converte o obj do I360 para o obj da Seguradora
                var seguradoraObj = _util.AdapterSaida(objT3, seguradora);

                //Encaminhar para a Seguradora
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
