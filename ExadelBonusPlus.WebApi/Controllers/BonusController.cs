using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ExadelBonusPlus.Services.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using ExadelBonusPlus.Services;

namespace ExadelBonusPlus.WebApi
{
    [ApiController]
    [Route("api/[controller]/")]
    [ValidationFilter]
    [ExceptionFilter]
    [HttpModelResultFilter]
    public class BonusController : ControllerBase
    {

        private readonly ILogger<BonusController> _logger;
        private readonly IBonusService _BonusService;
        
        public BonusController(ILogger<BonusController> logger, IBonusService BonusService)
        {
            _logger = logger;
            _BonusService = BonusService;

        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus added ", Type = typeof(HttpModel<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HttpModel<BonusDto>>> AddBonusAsync([FromBody] AddBonusDto Bonus)
        {
            return Ok(await _BonusService.AddBonusAsync(Bonus));
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "All bonus", Type = typeof(HttpModel<List<BonusDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HttpModel<IEnumerable<BonusDto>>>> FindAllBonusAsync()
        {
            return Ok(await _BonusService.FindAllBonusAsync());
        }

        [HttpGet]
        [Route("active")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "All active bonus", Type = typeof(HttpModel<List<BonusDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HttpModel<IEnumerable<BonusDto>>>> FindAllActiveBonusAsync()
        {
            return Ok(await _BonusService.FindAllActiveBonusAsync());
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus by ID", Type = typeof(HttpModel<List<BonusDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HttpModel<IEnumerable<BonusDto>>>> FindBonusByIdAsync([FromRoute] Guid id)
        {
            return Ok(await _BonusService.FindBonusByIdAsync(id));
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus updated ", Type = typeof(HttpModel<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HttpModel<BonusDto>>> UpdateBonusAsync([FromRoute] Guid id, [FromBody] BonusDto Bonus)
        {
            return Ok(await _BonusService.UpdateBonusAsync(id, Bonus));
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus deleted ", Type = typeof(HttpModel<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HttpModel<BonusDto>>> DeleteBonusAsync([FromRoute] Guid id)
        {
           return Ok(await _BonusService.DeleteBonusAsync(id));
        }

        [HttpPatch]
        [Route("{id}/activate")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus activated", Type = typeof(HttpModel<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HttpModel<BonusDto>>> ActivateBonusAsync([FromRoute] Guid id)
        {
            return Ok(await _BonusService.ActivateBonusAsync(id));
        }

        [HttpPatch]
        [Route("{id}/deactivate")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus deactivated", Type = typeof(HttpModel<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<HttpModel<BonusDto>>> DeactivateBonusAsync([FromRoute] Guid id)
        {
            return Ok(await _BonusService.DeactivateBonusAsync(id));
        }

    }
}