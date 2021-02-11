using System;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services.Models
{
    public interface IUserService
    {
        Task<AuthResponce> LogInAsync(LoginUserDTO loginUser, string ipAddress);
        Task RegisterAsync(RegisterUserDTO registerUser);
        Task LogOutAsync();
        Task<UserInfoDTO> GetUserAsync(string UserId);
        Task<UserInfoDTO> DeleteUserAsync(Guid UserId);
        Task<UserInfoDTO> RestoreUserAsync(Guid UserId);
        Task<UserInfoDTO> UpdateUserAsync(Guid UserId, UpdateUserDTO updateUserDto);

        Task<UserInfoDTO> AddRoleToUserAsync(string usersId, string roleName);
        Task<UserInfoDTO> RemoveUserRoleAsync(string usersId, string roleName);
        Task<AuthResponce> RefreshToken(string refreshToken, string ipAddress);
    }
}