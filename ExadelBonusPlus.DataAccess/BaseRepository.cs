using System;
using System.Collections.Generic;
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

        public virtual Task<TModel> AddAsync(TModel obj)
        {
            GetCollection().InsertOne(obj);
            return  Task.FromResult(obj);
        }
        
        public virtual Task<IEnumerable<TModel>> GetAllAsync()
        {
            return Task.FromResult(GetCollection().Find(Builders<TModel>.Filter.Empty).ToList() as IEnumerable<TModel>);
        }

        public virtual Task<TModel> GetByIdAsync(Guid id)
        {
            return GetCollection().Find(Builders<TModel>.Filter.Eq("_id", id)).FirstAsync();
        }

        public virtual Task UpdateAsync(Guid id, TModel obj)
        {
            return GetCollection().ReplaceOneAsync(Builders<TModel>.Filter.Eq("_id", id), obj);
        }

        public virtual Task RemoveAsync(Guid id)
        {
            return GetCollection().DeleteOneAsync(Builders<TModel>.Filter.Eq("_id", id));
        }
        
        private IMongoCollection<TModel> GetCollection()
        {
            return _database.GetCollection<TModel>(nameof(TModel));
        }
    }
}
