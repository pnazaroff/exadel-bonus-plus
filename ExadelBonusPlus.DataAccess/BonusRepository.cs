using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using ExadelBonusPlus.Services;
using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;

namespace ExadelBonusPlus.DataAccess
{
    public class BonusRepository : BaseRepository<Bonus>, IBonusRepository
    {
        public BonusRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {
        }
        public async Task<IEnumerable<Bonus>> GetAllActiveBonusAsync(CancellationToken cancellationToken)
        {
            return await GetCollection().Find(Builders<Bonus>.Filter.Eq(new ExpressionFieldDefinition<Bonus, bool>(x => x.IsDeleted), false) 
                                              & Builders<Bonus>.Filter.Eq(new ExpressionFieldDefinition<Bonus, bool>(x => x.IsActive), true)).ToListAsync(cancellationToken);
        }

        public Task<Bonus> ActivateBonusAsync(Guid id,  CancellationToken cancellationToken)
        {
            return GetCollection().FindOneAndUpdateAsync(Builders<Bonus>.Filter.Eq(new ExpressionFieldDefinition<Bonus, Guid>(x => x.Id), id),
                                                         Builders<Bonus>.Update.Set(new ExpressionFieldDefinition<Bonus, bool>(x => x.IsActive), true),
                new FindOneAndUpdateOptions<Bonus, Bonus>(){ ReturnDocument = ReturnDocument.After }, cancellationToken);
        }

        public Task<Bonus> DeactivateBonusAsync(Guid id, CancellationToken cancellationToken)
        {
            return GetCollection().FindOneAndUpdateAsync(Builders<Bonus>.Filter.Eq(new ExpressionFieldDefinition<Bonus, Guid>(x => x.Id), id),
                Builders<Bonus>.Update.Set(new ExpressionFieldDefinition<Bonus, bool>(x => x.IsActive), false),
                new FindOneAndUpdateOptions<Bonus, Bonus>() { ReturnDocument = ReturnDocument.After }, cancellationToken);
        }
    }
}