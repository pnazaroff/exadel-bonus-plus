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
    public class PromotionsController : ControllerBase
    {

        private readonly ILogger<PromotionsController> _logger;
        private readonly PromotionService _promotionService;
        
        public PromotionsController(ILogger<PromotionsController> logger)
        {
            _logger = logger;
            _promotionService = new PromotionService();

        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Promotions", Type = typeof(List<Promotion>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public  async Task<ActionResult<IEnumerable<Promotion>>> FindPromotions()
        {
            try
            {
                return Ok(await _promotionService.FindPromotionsAsync());
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

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Promotion added ", Type = typeof(PromotionDTO))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PromotionDTO>> AddPromotion([FromBody]PromotionDTO promotion)
        {
            try
            {
                return Ok(await _promotionService.AddPromotionAsync(promotion));
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

        [HttpPatch]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Promotion updated ", Type = typeof(PromotionDTO))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PromotionDTO>> UpdatePromotion([FromBody] PromotionDTO promotion)
        {
            try
            {
                return Ok(await _promotionService.UpdatePromotionAsync(promotion));
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
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Promotion deleted ", Type = typeof(PromotionDTO))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PromotionDTO>> DeletePromotion([FromRoute] Guid id)
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