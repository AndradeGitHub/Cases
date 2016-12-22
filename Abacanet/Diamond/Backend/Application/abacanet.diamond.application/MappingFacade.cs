using System;
using System.Collections.Generic;

using AutoMapper;

using abacanet.diamond.domain;
using abacanet.diamond.domain.model;
using abacanet.diamond.domain.repository;
using abacanet.diamond.infrastructure.common;
using abacanet.diamond.infrastructure.log;
using abacanet.diamond.infrastructure.persistence;
using abacanet.diamond.infrastructure.persistence.interfaces;

namespace abacanet.diamond.application
{
    public class MappingFacade
    {
        private static IUnitOfWork _unitOfWork;
        
        private static dynamic _repositoryFactory;

        public MappingFacade()
        {
            _unitOfWork = new UnitOfWork();
            
            _repositoryFactory = RepositoryFactory.CreateRepository<MappingDomainModel, MappingRepository>(_unitOfWork);
        }

        public MappingFacade(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            _repositoryFactory = RepositoryFactory.CreateRepository<MappingDomainModel, MappingRepository>(_unitOfWork);
        }

        public List<T> GelAllMapping<T>()
        {
            try
            {
                var resultMapping = _repositoryFactory.GetAll();

                //Convert DomainModel to ViewModel   
                Mapper.Initialize(cfg => cfg.CreateMap<MappingDomainModel, T>());
                var mappingView = Mapper.Map<List<MappingDomainModel>, List<T>>(resultMapping);

                return mappingView;
            }
            catch (Exception ex)
            {
                Log.RecordError(ex);

                throw (ex.InnerException);
            }
        }        
    }
}
