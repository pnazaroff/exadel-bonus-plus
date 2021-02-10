using System;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IRoleService
    {
        public Task<RoleDTO> AddRole(string roleName);
        public Task<RoleDTO> DeleteRole(Guid id);
        public Task<RoleDTO> UpdateRole(Guid id, string roleName);
    }
}
