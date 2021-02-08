using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using ExadelBonusPlus.Services;
using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace ExadelBonusPlus.DataAccess
{
    public class BonusRepository : BaseRepository<Bonus>, IBonusRepository
    {
        public BonusRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {
        }
        public async Task<IEnumerable<Bonus>> GetBonusesAsync(BonusFilter bonusFilter, CancellationToken cancellationToken)
        {
            FilterDefinition<Bonus> filter =
                Builders<Bonus>.Filter.Eq(new ExpressionFieldDefinition<Bonus, bool>(x => x.IsDeleted), false);

            var isActive = bonusFilter?.FilterBy?.IsActive;
            if(isActive != null)
                filter = filter & Builders<Bonus>.Filter.Eq(new ExpressionFieldDefinition<Bonus, bool>(x => x.IsActive),
                    (bool)isActive);

            var title = bonusFilter?.FilterBy?.Title;
            if (!String.IsNullOrEmpty(title))
                filter = filter & Builders<Bonus>.Filter.Regex(new ExpressionFieldDefinition<Bonus, string>(x => x.Title), new BsonRegularExpression(title));

            var tags = bonusFilter?.FilterBy?.Tags;
            if (tags != null && tags.Count>0)
                filter = filter & Builders<Bonus>.Filter.AnyIn(b => b.Tags, tags);

            var date = bonusFilter?.FilterBy?.Date;
            if (date != null)
                filter = filter & Builders<Bonus>.Filter.Lte(x => x.DateStart, date) &
                         Builders<Bonus>.Filter.Gte(x => x.DateEnd, date);

            var sortBy = bonusFilter?.SortBy ?? "Title";

            return await GetCollection().Find(filter).Sort(Builders<Bonus>.Sort.Ascending(sortBy)).ToListAsync(cancellationToken);
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

        public async Task<IEnumerable<string>> GetBonusTagsAsync(CancellationToken cancellationToken)
        {
            return await GetCollection().Distinct<string>("Tags", Builders<Bonus>.Filter.Eq(new ExpressionFieldDefinition<Bonus, bool>(x => x.IsDeleted), false)
                                                                  & Builders<Bonus>.Filter.Eq(new ExpressionFieldDefinition<Bonus, bool>(x => x.IsActive), true)).ToListAsync(cancellationToken);
        }
    }
}