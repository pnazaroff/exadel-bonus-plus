using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ExadelBonusPlus.Services;
using ExadelBonusPlus.WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService)); ;
        }
        [HttpPost]
        [Route("Authorize")]
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> Login(AuthRequest model)
        {
            try
            {
                var result = await _userService.LogIn(model.Email, model.Password);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        [SwaggerResponse((int) HttpStatusCode.OK)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register(AuthRequest model)
        {
            try
            {
                var result = _userService.Register(model.Email, model.Password);
                if (result.Result == "Created")
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
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
                return StatusCode(500);
            }

        }

        [HttpPost]
        [Authorize(Roles="User")]
        [Route("UserInfo")]
        [SwaggerResponse((int) HttpStatusCode.OK)]
        [SwaggerResponse((int) HttpStatusCode.BadRequest)]
        [SwaggerResponse((int) HttpStatusCode.Forbidden)]
        public IActionResult UserInfo()
        {
            try
            {
                var nameIdentifier = this.HttpContext.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (nameIdentifier != null)
                {
                    var userInfo = _userService.GetUserInfo(nameIdentifier.Value);
                    if (userInfo != null)
                    {
                        return Ok(userInfo);
                    }
                }

                return BadRequest();
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
                return StatusCode(500);
            }
           

        }
       
        [HttpPost]
        [Route("logout")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public IActionResult Logout()
        {
            try
           {
               var result = _userService.LogOutAsync();
               if (result.IsCompletedSuccessfully)
               {
                   return Ok();
               }
               else
               {
                   return BadRequest();
               }
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
               return StatusCode(500);
           }
        }
    }
}
