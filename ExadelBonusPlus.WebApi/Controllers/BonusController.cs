using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus added ", Type = typeof(ResultDto<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResultDto<BonusDto>>> AddBonusAsync([FromBody] AddBonusDto Bonus)
        {
            return Ok(await _BonusService.AddBonusAsync(Bonus));
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "All bonus", Type = typeof(ResultDto<List<BonusDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResultDto<IEnumerable<BonusDto>>>> FindAllBonusAsync()
        {
            return Ok(await _BonusService.FindAllBonusAsync());
        }

        [HttpGet]
        [Route("active")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "All active bonus", Type = typeof(ResultDto<List<BonusDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResultDto<IEnumerable<BonusDto>>>> FindAllActiveBonusAsync()
        {
            return Ok(await _BonusService.FindAllActiveBonusAsync());
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus by ID", Type = typeof(ResultDto<List<BonusDto>>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResultDto<IEnumerable<BonusDto>>>> FindBonusByIdAsync([FromRoute] Guid id)
        {
            return Ok(await _BonusService.FindBonusByIdAsync(id));
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus updated ", Type = typeof(ResultDto<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResultDto<BonusDto>>> UpdateBonusAsync([FromRoute][Required] Guid id, [FromBody][Required] BonusDto Bonus)
        {
            return Ok(await _BonusService.UpdateBonusAsync(id, Bonus));
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus deleted ", Type = typeof(ResultDto<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResultDto<BonusDto>>> DeleteBonusAsync([FromRoute] Guid id)
        {
           return Ok(await _BonusService.DeleteBonusAsync(id));
        }

        [HttpPatch]
        [Route("{id}/activate")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus activated", Type = typeof(ResultDto<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResultDto<BonusDto>>> ActivateBonusAsync([FromRoute] Guid id)
        {
            return Ok(await _BonusService.ActivateBonusAsync(id));
        }

        [HttpPatch]
        [Route("{id}/deactivate")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus deactivated", Type = typeof(ResultDto<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<ResultDto<BonusDto>>> DeactivateBonusAsync([FromRoute] Guid id)
        {
            return Ok(await _BonusService.DeactivateBonusAsync(id));
        }

    }
}