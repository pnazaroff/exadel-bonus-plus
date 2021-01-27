using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IRefreshTokenRepositry : IRepository<RefreshToken, Guid>
    {
        public Task<IEnumerable<RefreshToken>> GetByCreatorIdAsync(Guid CreatorId, CancellationToken cancellationToken = default);
    }
}
