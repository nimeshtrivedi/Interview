using AutoMapper;
using CanWeFixItService.Data;
using System.Linq;

namespace CanWeFixItApi.MappingProfiles
{
    public class CustomResolver : IMemberValueResolver<object, object, string, int?>
    {
        private readonly IDatabaseService _db;

        public CustomResolver(IDatabaseService db)
        {
            _db = db;
        }
        public int? Resolve(object source, object destination, string sourceMember, int? destMember, ResolutionContext context)
        {
            return _db.Instruments().GetAwaiter().GetResult().FirstOrDefault(d => d.Sedol == sourceMember)?.Id;
        }
    }
}
