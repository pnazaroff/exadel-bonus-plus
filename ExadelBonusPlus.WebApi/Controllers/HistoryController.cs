using System;
using System.Net;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    [ValidationFilter]
    [HttpModelResultFilter]
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "History added ", Type = typeof(ResultDto<HistoryDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HistoryDto>> AddBonusAsync([FromBody] HistoryDto history)
        {
            return Ok(await _historyService.AddHistory(new History()));
        }
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "History deleted ", Type = typeof(ResultDto<HistoryDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HistoryDto>> DeleteBonusAsync([FromRoute] Guid id)
        {
            return Ok(await _historyService.DeleteHistory(id));
        }
    }
}
