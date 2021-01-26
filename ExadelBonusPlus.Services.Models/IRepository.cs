using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IRepository<TModel, TId>
        where TModel : IEntity<TId>
    {
        Task AddAsync(TModel obj, CancellationToken cancellationToken);
        Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken);
        Task<TModel> GetByIdAsync(TId id, CancellationToken cancellationToken);
        Task UpdateAsync(TId id, TModel obj, CancellationToken cancellationToken);
        Task RemoveAsync(TId id, CancellationToken cancellationToken);
    }
}
