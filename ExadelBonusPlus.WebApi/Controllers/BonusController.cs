using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ExadelBonusPlus.Services.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;
using ExadelBonusPlus.Services;
using ExadelBonusPlus.Services.Models.DTO;
using ExadelBonusPlus.Services.Models.Interfaces;

namespace ExadelBonusPlus.WebApi
{
    [ApiController]
    [Route("api/[controller]/")]
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus added ", Type = typeof(BonusDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<BonusDto>> AddBonusAsync([FromBody] BonusDto Bonus)
        {
            try
            {
                return Ok(await _BonusService.AddBonusAsync(Bonus));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonuss", Type = typeof(List<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<BonusDto>>> FindAllBonussAsync()
        {
            try
            {
                return Ok(await _BonusService.FindAllBonussAsync());
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }

        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonuss", Type = typeof(List<BonusDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<BonusDto>>> FindBonusByIdAsync([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _BonusService.FindBonusByIdAsync(id));
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

        }

        [HttpPatch]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus updated ", Type = typeof(BonusDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<BonusDto>> UpdateBonusAsync([FromRoute] Guid id, [FromBody] BonusDto Bonus)
        {
            try
            {
                return Ok(await _BonusService.UpdateBonusAsync(id, Bonus));
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Bonus deleted ", Type = typeof(BonusDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<BonusDto>> DeleteBonusAsync([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _BonusService.DeleteBonusAsync(id));
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}