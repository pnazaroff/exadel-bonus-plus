using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services.Models.Interfaces
{
    public interface IPromotionService
    {
        Task<Promotion> AddPromotionAsync(Promotion model);

        Task<List<Promotion>> FindAllPromotionsAsync();

        Task<Promotion> FindPromotionByIdAsync(Guid id);

        Task<Promotion> UpdatePromotionAsync(Promotion model);

        Task<Promotion> DeletePromotionAsync(Guid id);
    }
}
