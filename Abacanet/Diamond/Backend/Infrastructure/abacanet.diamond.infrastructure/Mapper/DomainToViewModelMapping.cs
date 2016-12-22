using System.Collections.Generic;

//using abacanet.diamond.domain.model;
//using abacanet.diamond.webapi.Models;

using AutoMapper;

namespace abacanet.diamond.infrastructure.mapper
{
    public class DomainToViewModelMapping : Profile
    {
        public override string ProfileName => "DomainToViewModelMapping";

        protected override void Configure()
        {
            //Mapper.CreateMap<List<OrderDomainModel>, List<OrderViewModel>>();
        }
    }
}