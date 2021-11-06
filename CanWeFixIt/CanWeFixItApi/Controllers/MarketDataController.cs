using System.Collections.Generic;
using System.Threading.Tasks;
using CanWeFixItService.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v2/marketdata")]
    public class MarketDataController : ControllerBase
    {
        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> Get()
        {
            // TODO:

            return NotFound();
        }
    }
}