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
    public class BonusService : IBonusService
    {
        private readonly IBonusRepository _BonusRepository;
        private readonly IMapper _mapper;
        public BonusService(IBonusRepository promotinRepository, IMapper mapper)
        {
           _BonusRepository = promotinRepository;
           _mapper = mapper;
        }

        public async Task<BonusDto> AddBonusAsync(BonusDto model)
        {
            model.Id = Guid.NewGuid();
            var Bonus = _mapper.Map<Bonus>(model);
            await _BonusRepository.AddAsync(Bonus);
            return model;
        }

        public async Task<List<BonusDto>> FindAllBonussAsync()
        {
            var result = await _BonusRepository.GetAllAsync();
            return result is null ? throw new ArgumentException() : _mapper.Map<List<BonusDto>>(result);
        }

        public async Task<BonusDto> FindBonusByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new InvalidOperationException("Bad id");
            }
            var result = await _BonusRepository.GetByIdAsync(id);
            return result is null ? throw new ArgumentException("Bonus does not find by id") : _mapper.Map<BonusDto>(result);
        }

        public async Task<BonusDto> UpdateBonusAsync(Guid id, BonusDto model)
        {
            var Bonus = _mapper.Map<Bonus>(model);
            await _BonusRepository.UpdateAsync(id, Bonus);
            return model;
        }

        public async Task<BonusDto> DeleteBonusAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new InvalidOperationException("Bad id");
            }

            var result = await _BonusRepository.GetByIdAsync(id);
            if (result != null)
            {
                await _BonusRepository.RemoveAsync(id);
            }

            return result is null ? throw new ArgumentException("Does not find Bonus for delete") : _mapper.Map<BonusDto>(result);
        }
    }
}
