using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ExadelBonusPlus.Services.Models
{
    public abstract class BaseRepository<TModel> : IRepository<TModel> 
        where TModel : Entity
    {
        //protected readonly IMongoContext _context;
        protected readonly IMongoCollection<TEntity> _dbSet;

        public BaseRepository(IMongoContext Context)
        {
            //_context = Context;
            _dbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual void Add(TEntity obj)
        {
            //_context.AddCommand(() => _dbSet.InsertOneAsync(obj));
            _dbSet.InsertOneAsync(obj);
        }

        public void Dispose()
        {

        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await _dbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await _dbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public virtual async void Remove(Guid id)
        {
            //_context.AddCommand(() => _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
            await _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        }

        public virtual async void Update(Guid id, TEntity obj)
        {
            //_context.AddCommand(() => _dbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), obj));
            await _dbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), obj);
        }
    }
}
