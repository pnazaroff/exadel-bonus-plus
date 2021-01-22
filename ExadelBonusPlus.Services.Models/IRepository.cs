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
        Task<TModel> Add(TModel obj);
        Task<IEnumerable<TModel>> GetAll();
        Task<TModel> GetById(TId id);
        Task<TModel> Update(TId id, TModel obj);
        Task Remove(TId id);
    }
}
