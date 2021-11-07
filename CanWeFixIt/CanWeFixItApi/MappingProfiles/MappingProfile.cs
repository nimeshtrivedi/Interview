using AutoMapper;
using CanWeFixItService.Entities;
using CanWeFixItService.Entities.Dtos;

namespace CanWeFixItApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InstrumentDto, Instrument>().ReverseMap();
        }
    }
}
