using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.ViewModel;
using ExadelBonusPlus.WebApi.ViewModel;
using IdentityModel;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            _userService.RegisterAsync(registerUser);
            return Ok();
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var result = await _userService.LogInAsync(loginUser);
            return Ok(result);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogOutAsync();
            return Ok();
        }

        [HttpGet]
        [Route("getInfo")]
        public async Task<ActionResult<UserInfo>> GetUserInfoAsync()
        {
            var id = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _userService.GetUserAsync(id);
            return Ok(result);
        }

        
       [HttpDelete]
       [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserInfo>> DeleteUser(Guid userId)
        {
           return Ok(_userService.DeleteUser(userId).Result);
        }
    }
}
