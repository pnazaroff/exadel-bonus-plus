using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services.Models
{
    public interface IBonusRepository: IRepository<Bonus,Guid>
    {
        public Task<IEnumerable<Bonus>> GetAllActiveBonusAsync(CancellationToken cancellationToken);

        public Task<Bonus> ActivateBonusAsync(Guid id, CancellationToken cancellationToken);

        public Task<Bonus> DeactivateBonusAsync(Guid id, CancellationToken cancellationToken);
    }
}
