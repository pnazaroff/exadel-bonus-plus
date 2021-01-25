using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IRepository<TModel, TId>
        where TModel : IEntity<TId>
    {
        Task<TModel> AddAsync(TModel obj);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(TId id);
        Task UpdateAsync(TId id, TModel obj);
        Task RemoveAsync(TId id);
    }
}
