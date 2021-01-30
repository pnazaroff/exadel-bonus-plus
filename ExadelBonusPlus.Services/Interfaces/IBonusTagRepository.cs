using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services
{
    public interface IBonusTagRepository: IRepository<BonusTag,Guid>
    {
        public Task<BonusTag> FindTagByNameAsync(string bonusTagName, CancellationToken cancellationToken = default);
    }
}
