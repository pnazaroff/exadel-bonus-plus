using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface ITokenRefreshRepository : IRepository<TokenRefresh, Guid>
    {
        Task<IEnumerable<TokenRefresh>> GetRefreshTokenByIpAddress(string ipAddress, CancellationToken cancellationToken = default);
        Task<IEnumerable<TokenRefresh>> GetRefreshTokenByUserId(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<TokenRefresh>> GetRefreshTokenByToken(string token, CancellationToken cancellationToken = default);
    }
}
