using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using ExadelBonusPlus.WebApi.ViewModel;

namespace ExadelBonusPlus.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;
        private readonly IRefreshTokenRepositry _refreshTokenRepositry;
        public UserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IRefreshTokenRepositry refreshTokenRepositry,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager)); ;
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager)); ;
            _refreshTokenRepositry = refreshTokenRepositry ?? throw new ArgumentNullException(nameof(refreshTokenRepositry)); ;
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings.Value)); ;
        }

        public async Task<ApplicationUser> GetUserInfoAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }


        public async Task LogOutAsync()
        {
            var result = _signInManager.SignOutAsync();
        }

        public async Task<string> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser(email, email);
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    return "Created"; //Need callback for redirect login page
                }

                List<string> errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }

                return errorList.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        
        async Task<AuthResponce> IUserService.LogInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                if (result.Succeeded)
                {
                    try
                    {
                        var role = await _signInManager.UserManager.GetRolesAsync(user);
                        var token =  CreateToken(user, role);

                        var refreshToken = await _refreshTokenRepositry.GetByCreatorIdAsync(user.Id);
                        RefreshToken refresh;
                        if (refreshToken != null)
                        {
                            if (refreshToken.Any(x=>x.IsActive == true))
                            {
                                refresh = refreshToken.Where(x => x.IsActive == true).FirstOrDefault();
                                return new AuthResponce
                                {
                                    AccessToken = token,
                                    RefreshToken = refresh.Value
                                };
                            }
                            else
                            {
                                refresh = CreateRefreshToken(user);
                                await _refreshTokenRepositry.AddAsync(refresh);
                                return new AuthResponce
                                {
                                    AccessToken = token,
                                    RefreshToken = refresh.Value
                                };
                            }
                        }

                        
                        refresh = CreateRefreshToken(user);
                        await _refreshTokenRepositry.AddAsync(refresh);
                        return new AuthResponce
                        {
                            AccessToken = token,
                            RefreshToken = refresh.Value
                        };
                    }
                    catch (Exception e)
                    {
                        var m = e.Message;
                        throw new ArgumentNullException(nameof(AuthResponce));
                    }
                }
            }
            throw new ArgumentNullException(nameof(user));
        }

        public async Task<AuthResponce> RefreshAccessTokenAsync(string email, string refreshToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var role = await _signInManager.UserManager.GetRolesAsync(user);
                var oldRefreshToken = await _refreshTokenRepositry.GetByCreatorIdAsync(user.Id);
                var curruntRefreshToken = oldRefreshToken.First(x => x.Value == refreshToken);

                if (curruntRefreshToken.IsActive)
                {
                    var token = CreateToken(user, role);
                    curruntRefreshToken.ModifiedDate = DateTime.Now;
                    curruntRefreshToken.ModifierId = user.Id;
                    return new AuthResponce
                    {
                        AccessToken = token,
                        RefreshToken = curruntRefreshToken.Value
                    };
                }
            }
            catch (Exception e)
            {
                throw;
            }
          
            throw new ArgumentNullException(nameof(RefreshToken));
        }

        private string CreateToken(ApplicationUser applicationUser, IList<string> applicationRole)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
            };
            if (applicationRole.Count != 0)
            {
                foreach (var r in applicationRole)
                {
                    claims.Add(new Claim("role", r));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(_appSettings.Issuer, _appSettings.Issuer, claims,
                expires: DateTime.Now.AddMinutes(5), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        private RefreshToken CreateRefreshToken(ApplicationUser user)
        {
            using (var generator = new RNGCryptoServiceProvider())
            {
                var payload = Guid.NewGuid().ToString().Replace("-", "");
                return new RefreshToken
                {
                    CreatedDate = DateTime.Now,
                    CreatorId = user.Id,
                    Expires = DateTime.Now.AddDays(2),
                    Value = payload
                };
            }
        }

    }
}
