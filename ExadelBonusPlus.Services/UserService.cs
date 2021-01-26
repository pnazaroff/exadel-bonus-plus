using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ExadelBonusPlus.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;
        public UserService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager,
                           IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        public Task<ApplicationUser> GetUserInfo(string userId)
        {
            return  _userManager.FindByIdAsync(userId);
        }

        public async Task<string> LogIn(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (result.Succeeded)
                {
                    var token = await CreateTokenAsync(user);
                    return token;
                }
            }
            return null;   // return exeption
        }

        public async Task LogOutAsync()
        {
           var result = _signInManager.SignOutAsync();
        }

        public async Task<string> Register(string email, string password)
        {
            var user = new ApplicationUser(email, email);
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    return "Created";
                }
                else
                {
                    List<string> errorList = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errorList.Add(error.Description);
                    }

                    return errorList.ToString();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser applicationUser)
        {
            var role = await _signInManager.UserManager.GetRolesAsync(applicationUser);
            Claim claimRole = null;
            if (role.Count != 0)
            {
                string roles = "";
                foreach (var r in role)
                {
                    roles += r + " ";
                }

                claimRole = new Claim("role", role.ToString());
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
            };
            if (claimRole != null)
            {
                claims.Add(claimRole);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(_appSettings.Issuer, _appSettings.Issuer, claims,
                expires: DateTime.Now.AddMinutes(30), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
