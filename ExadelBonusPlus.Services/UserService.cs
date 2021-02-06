using ExadelBonusPlus.WebApi.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ExadelBonusPlus.Services.Models;
using IdentityModel;
using ExadelBonusPlus.WebApi.Controllers;
using JWT.Algorithms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using JWT.Builder;
using ExadelBonusPlus.Services.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace ExadelBonusPlus.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppJwtSettings _appJwtSettings;
        private readonly IMapper _mapper;
        public UserService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper,
            IOptions<AppJwtSettings> appJwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _appJwtSettings = appJwtSettings.Value;
        }
       
        public async Task<UserInfo> DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.IsActiv = false;
            var result =  await _userManager.UpdateAsync(user);

            return result is null ? throw new ArgumentException("") : _mapper.Map<UserInfo>(user);
        }

        public async Task<UserInfo> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user is null ? throw new ArgumentException("") : _mapper.Map<UserInfo>(user);
        }

        public async Task<AuthResponce> LogInAsync(LoginUser loginUser)
        {

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            var responce = new AuthResponce();
            responce.token = await GetFullJwtAsync(loginUser.Email);
            return responce;

        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task RegisterAsync(RegisterUser registerUser)
        {
            if (registerUser is null)
            {
                throw new ArgumentNullException("");
            }
            var user = new ApplicationUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,
                IsActiv = true,
                City = registerUser.City,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                PhoneNumber = registerUser.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (result.Succeeded)
                {
                    //Redirect("loginPath")
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<UserInfo> UpdateUser(Guid userId, RegisterUser registerUser)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.IsActiv = true;
            user.LastName = registerUser.LastName;
            user.City = registerUser.City;
            user.FirstName = registerUser.FirstName;
            user.PhoneNumber = registerUser.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);

            return result is null ? throw new ArgumentException("") : _mapper.Map<UserInfo>(user);
        }

        private async Task<string> GetFullJwtAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var roles = await _userManager.GetRolesAsync(user);
                return new JwtBuilder()
                    .WithAlgorithm(new HMACSHA512Algorithm())
                    .WithSecret(_appJwtSettings.SecretKey)
                    .AddClaim("sub", user.Id)
                    .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeSeconds())
                    .AddClaim(JwtClaimTypes.Email, user.Email)
                    .AddClaim(JwtClaimTypes.Audience, _appJwtSettings.Audience)
                    .AddClaim(JwtClaimTypes.Role, roles)
                    .AddClaim("isActiv", user.IsActiv)
                    .Encode();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
