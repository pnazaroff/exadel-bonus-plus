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
    public class PromotionController : ControllerBase
    {

        private readonly ILogger<PromotionController> _logger;
        private readonly IPromotionService _promotionService;
        
        public PromotionController(ILogger<PromotionController> logger, IPromotionService promotionService)
        {
            _logger = logger;
            _promotionService = promotionService;

        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Promotion added ", Type = typeof(PromotionDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PromotionDto>> AddPromotionAsync([FromBody] PromotionDto promotion)
        {
            try
            {
                return Ok(await _promotionService.AddPromotionAsync(promotion));
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Promotions", Type = typeof(List<PromotionDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> FindAllPromotionsAsync()
        {
            try
            {
                return Ok(await _promotionService.FindAllPromotionsAsync());
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Promotions", Type = typeof(List<PromotionDto>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<PromotionDto>>> FindPromotionByIdAsync([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _promotionService.FindPromotionByIdAsync(id));
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Promotion updated ", Type = typeof(PromotionDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PromotionDto>> UpdatePromotionAsync([FromRoute] Guid id, [FromBody] PromotionDto promotion)
        {
            try
            {
                return Ok(await _promotionService.UpdatePromotionAsync(id, promotion));
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Promotion deleted ", Type = typeof(PromotionDto))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PromotionDto>> DeletePromotionAsync([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _promotionService.DeletePromotionAsync(id));
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