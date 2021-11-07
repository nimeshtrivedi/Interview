using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CanWeFixItService.Data;
using CanWeFixItService.Entities;
using CanWeFixItService.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/instruments")]
    public class InstrumentController : ControllerBase
    {
        private readonly IDatabaseService _database;
        private readonly IMapper _mapper;

        public InstrumentController(IDatabaseService database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }
        
        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstrumentDto>>> Get()
        {   
            var instruments = await _database.Instruments();
            return Ok(_mapper.Map<IEnumerable<Instrument>, IEnumerable<InstrumentDto>>(instruments));
            
        }
    }
}