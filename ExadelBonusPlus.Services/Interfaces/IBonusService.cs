using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services
{
    public interface IBonusService
    {
        Task<BonusDto> AddBonusAsync(BonusDto model);

        Task<List<BonusDto>> FindAllBonusAsync();

        Task<BonusDto> FindBonusByIdAsync(Guid id);

        Task<BonusDto> UpdateBonusAsync(Guid id, BonusDto model);

        Task<BonusDto> DeleteBonusAsync(Guid id);
    }
}
