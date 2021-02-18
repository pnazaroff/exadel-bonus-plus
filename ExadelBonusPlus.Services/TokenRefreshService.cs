using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services
{
    public class TokenRefreshService : ITokenRefreshService
    {
        private readonly ITokenRefreshRepository _tokenRefreshRepository;
        public TokenRefreshService(ITokenRefreshRepository tokenRefreshRepository)
        {
            _tokenRefreshRepository = tokenRefreshRepository;
        }
        public async Task<TokenRefresh> GenerateRefreshToken(string ipAddress, Guid userId)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                var token = new TokenRefresh
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    CreatedDate = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                    CreatorId = userId
                };
                await _tokenRefreshRepository.AddAsync(token);
                return token;
            }
        }
        public async Task<IEnumerable<TokenRefresh>> GetRefreshTokenByIpAddress(string ipAddress)
        {
            return await _tokenRefreshRepository.GetRefreshTokenByIpAddress(ipAddress);
        }

        public async Task<IEnumerable<TokenRefresh>> GetRefreshTokenByToken(string token)
        {
            return await _tokenRefreshRepository.GetRefreshTokenByToken(token);
        }
        
        public async Task<TokenRefresh> UpdateRefreshToken(string ipAddress, TokenRefresh oldTokenRefresh)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                var token = new TokenRefresh
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    CreatedDate = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                    CreatorId = oldTokenRefresh.CreatorId
                };
                await _tokenRefreshRepository.AddAsync(token);
                
                oldTokenRefresh.Revoked = DateTime.Now;
                oldTokenRefresh.RevokedByIp = ipAddress;
                oldTokenRefresh.ReplacedByToken = token.Token;
                await _tokenRefreshRepository.UpdateAsync(oldTokenRefresh.Id, oldTokenRefresh);
                return token;
            }
        }
    }
}
