using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.DTO;
using ExadelBonusPlus.Services.Models.Interfaces;
using Microsoft.Extensions.Primitives;

namespace ExadelBonusPlus.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        public PromotionService(IPromotionRepository promotinRepository)
        {
        }

        public async Task<PromotionDTO> AddPromotionAsync(PromotionDTO model)
        {
            model.Id = new Guid();
            var promotion = Mapper.Map<Promotion>(model);
            await _promotionRepository.AddAsync(promotion, CancellationToken.None);
            return model;
        }

        public async Task<List<PromotionDTO>> FindAllPromotionsAsync()
        {
            var result = new List<PromotionDTO>();
            return result is null ? throw new InvalidOperationException("Find promotions error") : result;
        }

        public async Task<PromotionDTO> FindPromotionByIdAsync(Guid id)
        {
            var result = new PromotionDTO();
            return result is null ? throw new InvalidOperationException("Find promotion by Id error") : result;
        }

        public async Task<PromotionDTO> UpdatePromotionAsync(PromotionDTO model)
        {
            var result = new PromotionDTO();
            return result is null ? throw new InvalidOperationException("Update promotion error") : result;
        }

        public async Task<PromotionDTO> DeletePromotionAsync(Guid id)
        {
            var result = new PromotionDTO();
            return result is null ? throw new InvalidOperationException("Delete promotion error") : result;
        }
    }
}
