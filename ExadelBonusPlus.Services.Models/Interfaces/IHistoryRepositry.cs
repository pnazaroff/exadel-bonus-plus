using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IHistoryRepositry : IRepository<History, Guid>
    {
        public Task<IEnumerable<History>> GetUserHistory(Guid userId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<History>> GetBonusHistory(Guid bonusId, CancellationToken cancellationToken = default);
        public Task<IEnumerable<History>> GetHistoryByUsageDate(DateTime usageDateStart, DateTime usageDateEnd, CancellationToken cancellationToken = default);
    }
}
