using AutoMapper;
using CanWeFixItEntities;
using CanWeFixItEntities.Dtos;

namespace CanWeFixItApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Instrument, InstrumentDto>().ReverseMap();
            CreateMap<MarketValuation, MarketValuationDto>().ReverseMap();
            CreateMap<MarketData, MarketDataDto>()  //MarketDataDto expects InstrumentId based on Sedol Value -- Custom resolver will extract value from database
                .ForMember(dest => dest.InstrumentId, o =>
                {
                    o.MapFrom<CustomResolver, string>(src => src.Sedol);
                });
        }
    }
}
