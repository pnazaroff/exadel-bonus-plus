using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.DTO;
using ExadelBonusPlus.Services.Models.Interfaces;
using ExadelBonusPlus.Services.Properties;
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
            if (model is null)
            {
                throw new ArgumentNullException(Resources.ModelIsNull);
            }
            model.Id = Guid.NewGuid();
            var bonus = _mapper.Map<Bonus>(model);
            await _BonusRepository.AddAsync(bonus);
            return model;
        }

        public async Task<List<BonusDto>> FindAllBonusAsync()
        {
            var result = await _BonusRepository.GetAllAsync();
            return result is null ? throw new InvalidOperationException(Resources.FindError) : _mapper.Map<List<BonusDto>>(result);
        }

        public async Task<BonusDto> FindBonusByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(Resources.IdentifierIsNull);
            }
            var result = await _BonusRepository.GetByIdAsync(id);
            return result is null ? throw new ArgumentException(Resources.FindbyIdError) : _mapper.Map<BonusDto>(result);
        }

        public async Task<BonusDto> UpdateBonusAsync(Guid id, BonusDto model)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(Resources.IdentifierIsNull);
            }
            if (model is null)
            {
                throw new ArgumentNullException(Resources.ModelIsNull);
            }
            var bonus = _mapper.Map<Bonus>(model);
            await _BonusRepository.UpdateAsync(id, bonus);
            return model;
        }

        public async Task<BonusDto> DeleteBonusAsync(Guid id, bool softDelete = true)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(Resources.IdentifierIsNull);
            }

            var bonus = await _BonusRepository.GetByIdAsync(id);
            if (bonus is null)
            {
                throw new InvalidOperationException(Resources.FindbyIdError);
            }

            if (softDelete)
            {
                bonus.IsDeleted = true;
                await _BonusRepository.UpdateAsync(id, bonus);
            }
            else
            {
                await _BonusRepository.RemoveAsync(id);
            }

            return _mapper.Map<BonusDto>(bonus);
        }
    }
}
