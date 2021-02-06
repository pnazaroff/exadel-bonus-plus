using System;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models.ViewModel;
using ExadelBonusPlus.WebApi.ViewModel;

namespace ExadelBonusPlus.Services
{
    public interface IUserService
    {
        Task<AuthResponce> LogInAsync(LoginUser loginUser);

        Task RegisterAsync(RegisterUser registerUser);

        Task LogOutAsync();

        Task<UserInfo> GetUserAsync(string UserId);
        Task<UserInfo> DeleteUser(Guid UserId);
        Task<UserInfo> UpdateUser(Guid UserId, RegisterUser registerUser);
    }
}
