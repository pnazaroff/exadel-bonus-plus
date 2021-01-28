using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.DTO;

namespace ExadelBonusPlus.Services.Models.Interfaces
{
    public interface IBonusService
    {
        Task<BonusDto> AddBonusAsync(BonusDto model);

        Task<List<BonusDto>> FindAllBonussAsync();

        Task<BonusDto> FindBonusByIdAsync(Guid id);

        Task<BonusDto> UpdateBonusAsync(Guid id, BonusDto model);

        Task<BonusDto> DeleteBonusAsync(Guid id);
    }
}
