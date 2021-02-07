using ExadelBonusPlus.WebApi.ViewModel;
using System;
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

namespace ExadelBonusPlus.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppJwtSettings _appJwtSettings;
        private readonly IMapper _mapper;
        public UserService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IOptions<AppJwtSettings> appJwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _appJwtSettings = appJwtSettings.Value;
        }
        public async Task<UserInfoDTO> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user is null ? throw new ArgumentException("") : _mapper.Map<UserInfoDTO>(user);
        }
        public async Task<AuthResponce> LogInAsync(LoginUserDTO loginUser)
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user.IsActiv)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
                if (result.Succeeded)
                {
                    var responce = new AuthResponce();
                    responce.token = await GetFullJwtAsync(user);
                    return responce;
                }
                throw new ApplicationException("Wrong login or password");
            }
            throw new ApplicationException("This person is not an employee of the company");
        }
        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task RegisterAsync(RegisterUserDTO registerUser)
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
        public async Task<UserInfoDTO> DeleteUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.IsActiv = false;
            var result = await _userManager.UpdateAsync(user);

            return result is null ? throw new ArgumentException("") : _mapper.Map<UserInfoDTO>(user);
        }
        public async Task<UserInfoDTO> RestoreUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.IsActiv = true;
            var result = await _userManager.UpdateAsync(user);

            return result is null ? throw new ArgumentException("") : _mapper.Map<UserInfoDTO>(user);
        }
        public async Task<UserInfoDTO> UpdateUserAsync(Guid userId, UpdateUserDTO updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            user.IsActiv = true;
            user.LastName = updateUserDto.LastName;
            user.City = updateUserDto.City;
            user.FirstName = updateUserDto.FirstName;
            user.PhoneNumber = updateUserDto.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);

            return result is null ? throw new ArgumentException("") : _mapper.Map<UserInfoDTO>(user);
        }
        private async Task<string> GetFullJwtAsync(ApplicationUser user)
        {
            try
            {
                var roles = await _userManager.GetRolesAsync(user);
                return new JwtBuilder()
                    .WithAlgorithm(new HMACSHA512Algorithm())
                    .WithSecret(_appJwtSettings.SecretKey)
                    .AddClaim(JwtClaimTypes.Subject, user.Id)
                    .AddClaim(JwtClaimTypes.Expiration, DateTimeOffset.UtcNow.AddMinutes(_appJwtSettings.Expiration).ToUnixTimeSeconds())
                    .AddClaim(JwtClaimTypes.Email, user.Email)
                    .AddClaim(JwtClaimTypes.Audience, _appJwtSettings.Audience)
                    .AddClaim(JwtClaimTypes.Role, roles)
                    .Encode();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
