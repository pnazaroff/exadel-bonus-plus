using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services
{
    public interface ITokenRefreshService
    {
        Task<TokenRefresh> GenerateRefreshToken(string ipAddress,  Guid userId);
        Task<IEnumerable<TokenRefresh>> GetRefreshTokenByIpAddress(string ipAddress);
        Task<TokenRefresh> UpdateRefreshToken(string ipAddress, TokenRefresh oldTokenRefresh);
        Task<IEnumerable<TokenRefresh>> GetRefreshTokenByToken(string token);

    }
}
