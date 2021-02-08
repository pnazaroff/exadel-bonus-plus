using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface ITokenRefreshRepository : IRepository<TokenRefresh, Guid>
    {
        Task<TokenRefresh> GetRefreshTokenByIpAddress(string ipAddress);
    }
}
