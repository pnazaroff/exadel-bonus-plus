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
        public async Task<ActionResult<UserInfoDTO>> DeleteUserAsync([FromRoute] Guid id)
        {
            return Ok(( _userService.DeleteUserAsync(id)).Result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("user/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Restore user", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> RestoreUser([FromRoute] Guid id)
        {
            return Ok(_userService.RestoreUserAsync(userId).Result);
        }
        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("user/{id}/addroles")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Add role to user", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> AddRoleToUser([FromRoute] Guid id,  string roleName)
        {
            return Ok(_userService.AddRoleToUserAsync(id.ToString(), roleName).Result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("user/{id}/removeroles")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Remove role from user", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> RemoveUserRole([FromRoute] Guid id,  string roleName)
        {
            return Ok(_userService.RemoveUserRoleAsync(id.ToString(), roleName).Result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("user/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Update user info", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> UdpateUser([FromRoute] Guid id, [FromBody] UpdateUserDTO updateUserDto)
        {
            return Ok(_userService.UpdateUserAsync(id, updateUserDto).Result);
        }

        [HttpGet]
        [Route("user/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Update user info",
            Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<UserInfoDTO>> GetUserByIdAsync([FromRoute] Guid id)
        {
            var result = await _userService.GetUserAsync(id.ToString());
            return Ok(result);
        }

        [HttpDelete]
        [Route("role")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Delete role", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> DeleteRole(Guid id)
        {
            return Ok(_roleService.DeleteRole(id).Result);
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
        [Route("role/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "Update role ", Type = typeof(HttpModel<UserInfoDTO>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public ActionResult<UserInfoDTO> UdpateRole([FromRoute] Guid id , string roleName)
        {
            return Ok(_roleService.UpdateRole(id, roleName).Result);
        }
    }
}
