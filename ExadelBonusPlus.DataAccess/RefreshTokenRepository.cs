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
    public class RefreshTokenRepository:BaseRepository<RefreshToken>, IRefreshTokenRepositry
    {
        public RefreshTokenRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {
            
        }

        public async Task<IEnumerable<RefreshToken>> GetByCreatorIdAsync(Guid CreatorId, CancellationToken cancellationToken)
        {
            return await GetCollection().Find(Builders<RefreshToken>.Filter.Eq("CreatorId", CreatorId)).ToListAsync(cancellationToken);
        }
    }
}
