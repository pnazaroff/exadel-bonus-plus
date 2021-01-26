using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.Interfaces;

namespace ExadelBonusPlus.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        public PromotionService(IPromotionRepository promotinRepository)
        {
        }

        public async Task<Promotion> AddPromotionAsync(Promotion model)
        {
            var result = new Promotion();
            return result is null ? throw new InvalidOperationException("Add promotion error") : result;
        }

        public async Task<List<Promotion>> FindAllPromotionsAsync()
        {
            var result = new List<Promotion>();
            return result is null ? throw new InvalidOperationException("Find promotions error") : result;
        }

        public async Task<Promotion> FindPromotionByIdAsync(Guid id)
        {
            var result = new Promotion();
            return result is null ? throw new InvalidOperationException("Find promotion by Id error") : result;
        }

        public async Task<Promotion> UpdatePromotionAsync(Promotion model)
        {
            var result = new Promotion();
            return result is null ? throw new InvalidOperationException("Update promotion error") : result;
        }

        public async Task<Promotion> DeletePromotionAsync(Guid id)
        {
            var result = new Promotion();
            return result is null ? throw new InvalidOperationException("Delete promotion error") : result;
        }
    }
}
