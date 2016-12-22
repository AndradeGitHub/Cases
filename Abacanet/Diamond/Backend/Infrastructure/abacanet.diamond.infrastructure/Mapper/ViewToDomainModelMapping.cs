using AutoMapper;

//using abacanet.diamond.webapi.Models;
//using abacanet.diamond.domain.model;

namespace abacanet.diamond.infrastructure.mapper
{
    public class ViewToDomainModelMapping : Profile
    {
        public override string ProfileName
        {
            get { return "ViewToDomainModelMapping"; }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<OrderViewModel, OrderDomainModel>();            
        }
    }
}