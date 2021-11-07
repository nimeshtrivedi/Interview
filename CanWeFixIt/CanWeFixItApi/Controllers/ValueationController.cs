using CanWeFixItService.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixItApi.Controllers
{
    [Route("v1/valuations")]
    [ApiController]
    public class ValueationController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketValuationDto>>> Get()
        {
            //// TODO:
            return Ok(new List<MarketValuationDto>());
        }
    }
}
