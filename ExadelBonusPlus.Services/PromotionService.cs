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
        private readonly IMapper _mapper;
        public PromotionService(IPromotionRepository promotinRepository, IMapper mapper)
        {
           _promotionRepository = promotinRepository;
           _mapper = mapper;
        }

        public async Task<PromotionDto> AddPromotionAsync(PromotionDto model)
        {
            model.Id = Guid.NewGuid();
            var promotion = _mapper.Map<Promotion>(model);
            await _promotionRepository.AddAsync(promotion);
            return model;
        }

        public async Task<List<PromotionDto>> FindAllPromotionsAsync()
        {
            var result = await _promotionRepository.GetAllAsync();
            return result is null ? throw new ArgumentException() : _mapper.Map<List<PromotionDto>>(result);
        }

        public async Task<PromotionDto> FindPromotionByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new InvalidOperationException("Bad id");
            }
            var result = await _promotionRepository.GetByIdAsync(id);
            return result is null ? throw new ArgumentException("Promotion does not find by id") : _mapper.Map<PromotionDto>(result);
        }

        public async Task<PromotionDto> UpdatePromotionAsync(Guid id, PromotionDto model)
        {
            var promotion = _mapper.Map<Promotion>(model);
            await _promotionRepository.UpdateAsync(id, promotion);
            return model;
        }

        public async Task<PromotionDto> DeletePromotionAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new InvalidOperationException("Bad id");
            }

            var result = await _promotionRepository.GetByIdAsync(id);
            if (result != null)
            {
                await _promotionRepository.RemoveAsync(id);
            }

            return result is null ? throw new ArgumentException("Does not find promotion for delete") : _mapper.Map<PromotionDto>(result);
        }
    }
}
