using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IBonusRepository: IRepository<Bonus,Guid>
    {
        public Task<IEnumerable<Bonus>> GetBonusesAsync(BonusFilter bonusFilter, CancellationToken cancellationToken);
        public Task<Bonus> ActivateBonusAsync(Guid id, CancellationToken cancellationToken);
        public Task<Bonus> DeactivateBonusAsync(Guid id, CancellationToken cancellationToken);
        public Task<Bonus> UpdateBonusRatingAsync(Guid id, double rating, CancellationToken cancellationToken);
        public Task<IEnumerable<string>> GetBonusTagsAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<string>> GetCitiesAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<Bonus>> GetBonusStatisticAsync(BonusFilter bonusFilter, CancellationToken cancellationToken);
    }
}
