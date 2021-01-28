using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using ExadelBonusPlus.Services.Models.Interfaces;
using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;

namespace ExadelBonusPlus.DataAccess
{
    public class BonusTagRepository : BaseRepository<BonusTag>, IBonusTagRepository
    {
        public BonusTagRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {
        }

        public async Task<BonusTag> FindTagByNameAsync(string bonusTagName, CancellationToken cancellationToken = default)
        {
            return await base.GetCollection().Find(Builders<BonusTag>.Filter.Eq("Name", bonusTagName)).FirstAsync(cancellationToken);
        }
    }
}