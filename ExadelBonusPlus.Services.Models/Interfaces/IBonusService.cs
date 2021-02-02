using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services.Models
{
    public interface IBonusService
    {
        Task<BonusDto> AddBonusAsync(AddBonusDto model, CancellationToken cancellationToken = default);

        Task<List<BonusDto>> FindAllBonusAsync(CancellationToken cancellationToken = default);

        Task<BonusDto> FindBonusByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<BonusDto> UpdateBonusAsync(Guid id, BonusDto model, CancellationToken cancellationToken = default);

        Task<BonusDto> DeleteBonusAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
