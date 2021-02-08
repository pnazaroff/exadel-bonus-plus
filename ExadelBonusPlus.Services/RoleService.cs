using System;
using System.Threading.Tasks;
using AutoMapper;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.RoleDTO;
using Microsoft.AspNetCore.Identity;

namespace ExadelBonusPlus.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<ApplicationRole> roleManager,
                           IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<RoleDTO> AddRole(string roleName)
        {
            ApplicationRole newRole = new ApplicationRole
            {
                Name = roleName
            };
            var result = await _roleManager.CreateAsync(newRole);
            return result is null ? throw new ArgumentException("") : _mapper.Map<RoleDTO>(newRole);
        }

        public async Task<RoleDTO> DeleteRole(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role is null)
            {
                throw new ArgumentNullException(nameof(ApplicationRole));
            }
            var result = await _roleManager.DeleteAsync(role);
            return result is null ? throw new ArgumentException("") : _mapper.Map<RoleDTO>(role);
        }

        public async Task<RoleDTO> UpdateRole(Guid id, string roleName)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role is null)
            {
                throw new ArgumentNullException(nameof(ApplicationRole));
            }
            role.Name = roleName;
            var result = await _roleManager.UpdateAsync(role);
            return result is null ? throw new ArgumentException("") : _mapper.Map<RoleDTO>(role);
        }
    }
}
