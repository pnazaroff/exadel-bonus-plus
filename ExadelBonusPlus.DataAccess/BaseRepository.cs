using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ExadelBonusPlus.Services.Models
{
    public abstract class BaseRepository<TModel> : IRepository<TModel, Guid> 
        where TModel : IEntity<Guid>
    {
        private readonly MongoDbSettings _mongoDbSettings;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        protected private BaseRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings.Value;
            _mongoClient = new MongoClient(_mongoDbSettings.ConnectionString);
            _database = _mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
        }

        public virtual Task AddAsync(TModel model, CancellationToken cancellationToken)
        {
            return GetCollection().InsertOneAsync(model, cancellationToken);
        }
        
        public virtual async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await GetCollection().Find(Builders<TModel>.Filter.Eq("IsDeleted", false)).ToListAsync(cancellationToken);
        }

        public virtual Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var idFilter = Builders<TModel>.Filter.Eq("id", id);
            var deletionFilter = Builders<TModel>.Filter.Eq("IsDeleted", false);

            return GetCollection().Find(Builders<TModel>.Filter.And(idFilter, deletionFilter)).FirstAsync(cancellationToken);
        }

        public virtual Task UpdateAsync(Guid id, TModel obj,  CancellationToken cancellationToken)
        {
            return GetCollection().ReplaceOneAsync(
                Builders<TModel>.Filter.Eq("_id", id),
                obj, 
                new ReplaceOptions() { IsUpsert = false },
                cancellationToken
                );
        }

        public virtual Task<TModel> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            //write filters 
            var idFilter = Builders<TModel>.Filter.Eq("id", id);
            var softDeletionDefinition = Builders<TModel>.Update.Set("IsDeleted", true);
            var options = new FindOneAndUpdateOptions<TModel, TModel>();

            return GetCollection().FindOneAndUpdateAsync(idFilter, softDeletionDefinition, options, cancellationToken);
        }
        
        protected private IMongoCollection<TModel> GetCollection()
        {
            return _database.GetCollection<TModel>(typeof(TModel).Name);
        }
    }
}
