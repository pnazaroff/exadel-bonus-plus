using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.DTO;

namespace ExadelBonusPlus.Services.Models.Interfaces
{
    public interface IPromotionService
    {
        Task<PromotionDTO> AddPromotionAsync(PromotionDTO model);

        Task<List<PromotionDTO>> FindAllPromotionsAsync();

        Task<PromotionDTO> FindPromotionByIdAsync(Guid id);

        Task<PromotionDTO> UpdatePromotionAsync(PromotionDTO model);

        Task<PromotionDTO> DeletePromotionAsync(Guid id);
    }
}
