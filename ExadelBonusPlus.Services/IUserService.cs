using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services
{
    public interface IUserService
    {
        Task<string> LogIn(string email, string password);
        Task LogOutAsync();
        Task<string> Register(string email, string password);

        Task<ApplicationUser> GetUserInfo(string userId);
    }
}
