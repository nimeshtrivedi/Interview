using CanWeFixItService.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItService.Data
{
    public interface IDatabaseService
    {
        Task<IEnumerable<Instrument>> Instruments();
        Task<IEnumerable<MarketData>> MarketData();
        Task<IEnumerable<MarketValuation>> MarketValuations();
        void SetupDatabase();
    }
}