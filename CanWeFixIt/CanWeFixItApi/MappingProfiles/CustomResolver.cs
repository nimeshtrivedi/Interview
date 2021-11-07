using AutoMapper;
using CanWeFixItEntities;
using CanWeFixItEntities.Dtos;
using CanWeFixItService.Data;
using System.Linq;

namespace CanWeFixItApi.MappingProfiles
{
    public class CustomResolver : IMemberValueResolver<MarketData, MarketDataDto, string, int?>
    {
        private readonly IDatabaseService _db;

        public CustomResolver(IDatabaseService db)
        {
            _db = db;
        }
        public int? Resolve(MarketData source, MarketDataDto destination, string sourceMember, int? destMember, ResolutionContext context)
        {
            return _db.Instruments().GetAwaiter().GetResult().FirstOrDefault(d => d.Sedol == sourceMember)?.Id;
        }
    }
}
