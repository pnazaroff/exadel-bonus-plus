using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IRepository<TModel>
        where TModel : Entity
    {
        Task<TModel> Add(TModel obj);
        Task<IEnumerable<TModel>> Find(Expression<Func<TModel, bool>> filter);
        Task<TModel> Update(Guid id, TModel obj);
        Task<TModel> Remove(Guid id);
    }
}
