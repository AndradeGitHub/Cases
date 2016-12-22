using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

using AutoMapper;

using abacanet.diamond.domain;
using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository;
using abacanet.diamond.infrastructure.files;
using abacanet.diamond.infrastructure.log;
using abacanet.diamond.infrastructure.persistence;
using abacanet.diamond.infrastructure.persistence.interfaces;
using abacanet.diamond.infrastructure.exceptions;

namespace abacanet.diamond.application
{
    public class ProfitAndLossFacade
    {
        private static IUnitOfWork _unitOfWork;

        private static dynamic _repositoryFactory;

        public ProfitAndLossFacade()
        {
            _unitOfWork = new UnitOfWork();

            _repositoryFactory = RepositoryFactory.CreateRepository<ProfitAndLossModel, ProfitAndLossRepository>(_unitOfWork);
        }

        public ProfitAndLossFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _repositoryFactory = RepositoryFactory.CreateRepository<ProfitAndLossModel, ProfitAndLossRepository>(_unitOfWork);
        }

        public void UploadFile(string fileName)
        {
            try
            {
                var excelReader = new ExcelReader(fileName);
                var ret = excelReader.Read();

                if (ret == null)
                    return;

                _repositoryFactory.Add(ret);
                _unitOfWork.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                Log.RecordError(ex);

                var entityError = EntityValidationException.Validate(ex);

                throw new DbEntityValidationException(entityError.Item1, entityError.Item2);
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);

                throw (ex.InnerException);
            }
        }
    }
}
