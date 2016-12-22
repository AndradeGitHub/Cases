using AutoMapper;

namespace abacanet.diamond.infrastructure.mapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {            
            Mapper.Initialize(x =>
            {
                //x.CreateMap<OrderViewModel, OrderDomainModel>();
                //x.CreateMap<OrderDomainModel, OrderViewModel>();
            });
        }
    }
}