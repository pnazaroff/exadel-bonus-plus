using System;
using System.Net;
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
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AdminController(IUserService userService,
                                IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        [HttpDelete]
        [Route("user/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Delete user", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserInfoDTO>> DeleteUserAsync([FromRoute] Guid userId)
        {
            return Ok(( _userService.DeleteUserAsync(userId)).Result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("user/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Restore user", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> RestoreUser([FromRoute] Guid userId)
        {
            return Ok(_userService.RestoreUserAsync(userId).Result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("user/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Update user info", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> UdpateUser([FromRoute] Guid userId, [FromBody] UpdateUserDTO updateUserDto)
        {
            return Ok(_userService.UpdateUserAsync(userId, updateUserDto).Result);
        }

        [HttpGet]
        [Route("user/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Update user info",
            Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserInfoDTO>> GetUserByIdAsync([FromRoute] Guid userId)
        {
            var result = await _userService.GetUserAsync(userId.ToString());
            return Ok(result);
        }

        [HttpDelete]
        [Route("role")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Delete role", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> DeleteRole(string roleName)
        {
            return Ok(_roleService.DeleteRole(roleName).Result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("role")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Create role", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> CreateRole(string roleName)
        {
            return Ok(_roleService.AddRole(roleName).Result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("role")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Update role ", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> UdpateRole(string roleName)
        {
            return Ok(_roleService.DeleteRole(roleName).Result);
        }
    }
}
