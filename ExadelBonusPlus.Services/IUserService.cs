using System;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services
{
    public interface IUserService
    {
        Task<AuthResponce> LogInAsync(LoginUserDTO loginUser);
        Task RegisterAsync(RegisterUserDTO registerUser);
        Task LogOutAsync();
        Task<UserInfoDTO> GetUserAsync(string UserId);
        Task<UserInfoDTO> DeleteUserAsync(Guid UserId);
        Task<UserInfoDTO> RestoreUserAsync(Guid UserId);
        Task<UserInfoDTO> UpdateUserAsync(Guid UserId, UpdateUserDTO updateUserDto);
    }
}
