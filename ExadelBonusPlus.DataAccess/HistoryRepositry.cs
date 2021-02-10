using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExadelBonusPlus.DataAccess
{
    public class HistoryRepositry:BaseRepository<History>, IHistoryRepositry
    {
        public HistoryRepositry(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {
        }

        public async Task<IEnumerable<History>> GetBonusHistory(Guid bonusId, CancellationToken cancellationToken)
        {
            return await GetCollection().Find(Builders<History>.Filter.Eq("BonusId", bonusId)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<History>> GetHistoryByUsageDate(DateTime usageDate, DateTime usageDateEnd, CancellationToken cancellationToken)
        {
            var filterLess = Builders<History>.Filter.Lte("CreatedDate", usageDateEnd);
            var filterMore = Builders<History>.Filter.Gte("CreatedDate", usageDate);
            return await GetCollection().Find(filterLess & filterMore).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<History>> GetUserHistory(Guid userId, CancellationToken cancellationToken)
        {
            return await GetCollection().Find(Builders<History>.Filter.Eq("UserId", userId)).ToListAsync(cancellationToken);
        }
    }
}
