using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AdminController(RoleManager<ApplicationRole> roleManager,
                               UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            
            ApplicationRole newRole = new ApplicationRole
            {
                Name = roleName
            };
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                return Ok("Role created");
            }
            else
            {
                List<string> errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }

                return BadRequest(errorList);
            }

        }

        [HttpPut]
        [Route("role")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]

        public async Task<IActionResult> EditUserInRole(string usersName, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound("NotFound role with id = " + roleId);
            }
            else
            {
                var userNew = await _userManager.FindByEmailAsync(usersName);
                IdentityResult result = null;
                if (await _userManager.IsInRoleAsync(userNew, role.Name))
                {
                    return BadRequest("User in the choose role");
                }

                result = await _userManager.AddToRoleAsync(userNew, role.Name);
                if (!result.Succeeded)
                {
                    List<string> errorList = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errorList.Add(error.Description);
                    }
                    return BadRequest(errorList);
                }

                return BadRequest(userNew.UserName + "in Role:" + role.Name);
            }
        }

        [HttpDelete]
        [Route("role")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteRole(Guid roleId)
        {

            if (User.HasClaim(ClaimTypes.Role, "Admin"))
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role == null)
                {
                    return NotFound("NotFound role with id = " + roleId.ToString());
                }

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok("Role is deleted");
                }

                List<string> errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }

                return BadRequest(errorList);
            }
            return StatusCode(403);
        }

    }
}

