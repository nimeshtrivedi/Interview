using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CanWeFixItService.Data;
using CanWeFixItService.Entities;
using CanWeFixItService.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/marketdata")]
    public class MarketDataController : ControllerBase
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public MarketDataController(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> Get()
        {
            // TODO:
            var marketDatas = await _database.MarketData();
            return Ok(_mapper.Map<IEnumerable<MarketData>, IEnumerable<MarketDataDto>>(marketDatas).Where(a => a.InstrumentId != null));
        }
    }
}