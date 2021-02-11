using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IRefreshTokenRepositry : IRepository<TokenRefresh, Guid>
    {
        public Task<IEnumerable<TokenRefresh>> GetByCreatorIdAsync(Guid creatorId, CancellationToken cancellationToken = default);
    }
}
