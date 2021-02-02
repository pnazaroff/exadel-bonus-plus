using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.WebApi.ViewModel;

namespace ExadelBonusPlus.Services
{
    public interface IUserService
    {
        Task<AuthResponce> LogInAsync(string email, string password);
        Task LogOutAsync();
        Task<string> RegisterAsync(string email, string password);
        Task<AuthResponce> RefreshAccessTokenAsync(string email, string refreshToken);
        Task<ApplicationUser> GetUserInfoAsync(string userId);
    }
}