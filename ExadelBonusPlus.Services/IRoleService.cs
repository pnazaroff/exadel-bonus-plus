using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models.RoleDTO;

namespace ExadelBonusPlus.Services
{
    public interface IRoleService
    {
        public Task<RoleDTO> AddRole(string roleName);
        public Task<RoleDTO> DeleteRole(string roleName);
        public Task<RoleDTO> UpdateRole(string roleName);
    }
}
