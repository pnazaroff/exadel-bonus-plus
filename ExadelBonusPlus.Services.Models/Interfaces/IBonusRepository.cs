using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services.Models.Interfaces
{
    public interface IBonusRepository: IRepository<Bonus,Guid>
    {
        public new Task<IEnumerable<Bonus>> GetAllAsync(CancellationToken cancellationToken);

        public new Task<Bonus> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<Bonus>> FindBonusByTagAsync(string tag, CancellationToken cancellationToken);

        //to be continued ...
    }
}
