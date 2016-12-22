using abacanet.diamond.domain;
using abacanet.diamond.domain.model;
using abacanet.diamond.domain.model.Mappers;
using abacanet.diamond.domain.repository;
using abacanet.diamond.infrastructure.persistence;
using abacanet.diamond.infrastructure.persistence.interfaces;
using AutoMapper;
using System.Collections.Generic;

namespace abacanet.diamond.application
{
    public class MapperFacade
    {
        private static IUnitOfWork _unitOfWork;
        private static dynamic _repositoryFactory;

        public MapperFacade()
        {
            _unitOfWork = new UnitOfWork();
            _repositoryFactory = RepositoryFactory.CreateRepository<ProfitAndLossModel, ProfitAndLossRepository>(_unitOfWork);
        }

        public IEnumerable<IEnumerable<T>> Get<T>()
        {
            var afr15s = new List<List<T>>();

            var profitsandlosses = _repositoryFactory.GetAll();

            Mapper.Initialize(cfg => cfg.CreateMap<MapperResponse, T>());
            var mapper = new AFR15Mapper();
            foreach (var profitandloss in profitsandlosses)
            {
                var result = mapper.Map(profitandloss);
                afr15s.Add(Mapper.Map<IEnumerable<MapperResponse>, IEnumerable<T>>(result));
            }

            return afr15s;
        }
    }
}
