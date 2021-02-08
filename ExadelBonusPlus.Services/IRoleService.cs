using System;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models.RoleDTO;

namespace ExadelBonusPlus.Services
{
    public interface IRoleService
    {
        public Task<RoleDTO> AddRole(string roleName);
        public Task<RoleDTO> DeleteRole(Guid id);
        public Task<RoleDTO> UpdateRole(Guid id, string roleName);
    }
}
