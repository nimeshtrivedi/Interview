using AutoMapper;
using CanWeFixItService.Data;
using CanWeFixItService.Entities;
using CanWeFixItService.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Controllers
{
    [Route("v1/valuations")]
    [ApiController]
    public class ValuationController : ControllerBase
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public ValuationController(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketValuationDto>>> Get()
        {
            var marketValuations = await _database.MarketValuations();
            return Ok(_mapper.Map<IEnumerable<MarketValuation>, IEnumerable<MarketValuationDto>>(marketValuations));
        }
    }
}
