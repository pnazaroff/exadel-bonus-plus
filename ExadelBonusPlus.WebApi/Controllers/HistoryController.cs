using System;
using System.Net;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Interfaces;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        private readonly ILogger<HistoryController> _logger;

        public HistoryController(ILogger<HistoryController> logger, IHistoryService historyService)
        {
            _historyService = historyService;
            _logger = logger;
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "History added ", Type = typeof(HistoryDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HistoryDto>> AddBonusAsync([FromBody] HistoryDto history)
        {
            try
            {
                return Ok(await _historyService.AddHistory(new History()));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "History deleted ", Type = typeof(HistoryDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HistoryDto>> DeleteBonusAsync([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _historyService.DeleteHistory(id));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
