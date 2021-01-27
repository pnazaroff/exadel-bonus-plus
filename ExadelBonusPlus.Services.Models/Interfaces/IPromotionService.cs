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
        Task<PromotionDto> AddPromotionAsync(PromotionDto model);

        Task<List<PromotionDto>> FindAllPromotionsAsync();

        Task<PromotionDto> FindPromotionByIdAsync(Guid id);

        Task<PromotionDto> UpdatePromotionAsync(Guid id, PromotionDto model);

        Task<PromotionDto> DeletePromotionAsync(Guid id);
    }
}
