using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExadelBonusPlus.DataAccess
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        void Update(Guid id, TEntity obj);
        void Remove(Guid id);
    }
}
