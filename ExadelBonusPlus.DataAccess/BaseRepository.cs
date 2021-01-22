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

        private IMongoCollection<TModel> dbSet()
        {
            return _database.GetCollection<TModel>(nameof(TModel));
        }

        public virtual async Task<TModel> Add(TModel obj)
        {
            var result = dbSet().InsertOneAsync(obj);
            return obj;
        }
        
        public virtual async Task<IEnumerable<TModel>> GetAll()
        {
            return dbSet().Find(Builders<TModel>.Filter.Empty).ToList();
        }

        public virtual async Task<TModel> GetById(Guid id)
        {
            return dbSet().Find(Builders<TModel>.Filter.Eq("_id", id)).First();
        }

        public virtual async Task<TModel> Update(Guid id, TModel obj)
        {
            var result = dbSet().ReplaceOne(Builders<TModel>.Filter.Eq("_id", id), obj);
            return obj;
        }

        public virtual async Task Remove(Guid id)
        {
            dbSet().DeleteOneAsync(Builders<TModel>.Filter.Eq("_id", id));
        }
    }
}
