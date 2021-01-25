using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ExadelBonusPlus.Services.Models
{
    public abstract class BaseRepository<TModel> : IRepository<TModel, Guid> 
        where TModel : IEntity<Guid>
    {
        private readonly MongoDbSettings _mongoDbSettings;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public BaseRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            _mongoDbSettings = mongoDbSettings.Value;
            _mongoClient = new MongoClient(_mongoDbSettings.ConnectionString);
            _database = _mongoClient.GetDatabase(_mongoDbSettings.DatabaseName);
        }

        public virtual Task AddAsync(TModel obj, CancellationToken cancellationToken = default)
        {
            return GetCollection().InsertOneAsync(obj);
        }
        
        public virtual async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetCollection().Find(Builders<TModel>.Filter.Empty).ToListAsync(cancellationToken);
        }

        public virtual Task<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return GetCollection().Find(Builders<TModel>.Filter.Eq("_id", id)).FirstAsync(cancellationToken);
        }

        public virtual Task UpdateAsync(Guid id, TModel obj,  CancellationToken cancellationToken = default)
        {
            return GetCollection().ReplaceOneAsync(Builders<TModel>.Filter.Eq("_id", id), obj, new UpdateOptions { IsUpsert = true }, cancellationToken);
        }

        public virtual Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return GetCollection().DeleteOneAsync(Builders<TModel>.Filter.Eq("_id", id), cancellationToken);
        }
        
        private IMongoCollection<TModel> GetCollection()
        {
            return _database.GetCollection<TModel>(nameof(TModel));
        }
    }
}
