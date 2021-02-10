using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface ITokenRefreshService
    {
        Task<TokenRefresh> GenerateRefreshToken(string ipAddress,  Guid userId);
        Task<IEnumerable<TokenRefresh>> GetRefreshTokenByIpAddress(string ipAddress);
        Task<TokenRefresh> UpdateRefreshToken(string ipAddress, TokenRefresh oldTokenRefresh);
        Task<IEnumerable<TokenRefresh>> GetRefreshTokenByToken(string token);

    }
}
