using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ExadelBonusPlus.Services;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUserDTO registerUser)
        {
            await _userService.RegisterAsync(registerUser);
            return Ok();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = await _userService.LogInAsync(loginUser, ipAddress);
            return Ok(result);
        }

        [HttpPost]
        [Route("logout")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogOutAsync();
            return Ok();
        }

        [HttpGet]
        [Route("getInfo")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Get info about authorized user", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserInfoDTO>> GetUserInfoAsync()
        {
            var id = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _userService.GetUserAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("tokenrefresh")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "refresh your token", Type = typeof(HttpModel<AuthResponce>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponce>> GetRefreshToken( string refreshToken)
        {
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = await _userService.RefreshToken(refreshToken, ipAddress);
            return Ok(result);
        }
    }
}
