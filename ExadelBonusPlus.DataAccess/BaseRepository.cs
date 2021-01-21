using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ExadelBonusPlus.Services.Models
{
    public abstract class BaseRepository<TModel> : IRepository<TModel> 
        where TModel : Entity
    {

        private readonly IConfiguration _configuration;
        private readonly MongoClient MongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<TModel> _dbSet;

        
        public BaseRepository(IConfiguration configuration)
        {
            MongoClient = new MongoClient(_configuration["MongoDbSettings:ConnectionString"]);

            _database = MongoClient.GetDatabase(_configuration["MongoDbSettings:DatabaseName"]);
            _dbSet = _database.GetCollection<TModel>(nameof(TModel));
        }

        public virtual async Task<TModel> Add(TModel obj)
        {
            var result = _dbSet.InsertOneAsync(obj);
            return await _dbSet.Find(new BsonDocument("_id", obj.Id)).FirstAsync();
        }
        
        public virtual async Task<IEnumerable<TModel>> Find(Expression<Func<TModel, bool>> filter)
        {
            var all = await _dbSet.FindAsync(Builders<TModel>.Filter.Empty);
            return all.ToList();
        }


        public virtual async Task<TModel> Remove(Guid id)
        {
            var model = _dbSet.Find(new BsonDocument("_id", id)).FirstAsync();
            var result = _dbSet.DeleteOneAsync(Builders<TModel>.Filter.Eq("_id", id));
            return await model;
        }

        public virtual async Task<TModel> Update(Guid id, TModel obj)
        {
            var result = _dbSet.ReplaceOneAsync(Builders<TModel>.Filter.Eq("_id", id), obj);
            return await _dbSet.Find(new BsonDocument("_id", id)).FirstAsync();
        }
    }
}
