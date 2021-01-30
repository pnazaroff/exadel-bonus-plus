using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Properties;
using Microsoft.Extensions.Primitives;

namespace ExadelBonusPlus.Services
{
    public class BonusService : IBonusService
    {
        private readonly IBonusRepository _bonusRepository;
        private readonly IBonusTagRepository _bonusTagRepository;
        private readonly IMapper _mapper;
        public BonusService(IBonusRepository bonusRepository, IBonusTagRepository bonusTagRepository, IMapper mapper)
        {
            _bonusRepository = bonusRepository;
           _bonusTagRepository = bonusTagRepository;
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
            await _bonusRepository.AddAsync(bonus);
            await AddTagInCollection(bonus);
            return model;
        }

        public async Task<List<BonusDto>> FindAllBonusAsync()
        {
            var result = await _bonusRepository.GetAllAsync();
            return result is null ? throw new InvalidOperationException(Resources.FindError) : _mapper.Map<List<BonusDto>>(result);
        }

        public async Task<BonusDto> FindBonusByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(Resources.IdentifierIsNull);
            }
            var result = await _bonusRepository.GetByIdAsync(id);
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

            await _bonusRepository.UpdateAsync(id, bonus);
            await AddTagInCollection(bonus);

            return model;
        }

        public async Task<BonusDto> DeleteBonusAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(Resources.IdentifierIsNull);
            }

            var result =await _bonusRepository.RemoveAsync(id);

            return _mapper.Map<BonusDto>(result);
        }

        private async Task AddTagInCollection(Bonus bonus)
        {
            foreach (string stringBonusTag in bonus.Tags)
            {
                BonusTag bonusTag;
                bool isFind = false;

                try
                {
                    bonusTag = await _bonusTagRepository.FindTagByNameAsync(stringBonusTag);
                    isFind = true;
                }
                catch
                {
                    bonusTag = new BonusTag() {Name = stringBonusTag};
                }

                if (isFind)
                {
                    bonusTag.BonusIdList.Add(bonus.Id);
                    await _bonusTagRepository.UpdateAsync(bonusTag.Id, bonusTag);
                }
                else
                {
                    bonusTag.BonusIdList = new List<Guid>() { bonus.Id };
                    await _bonusTagRepository.AddAsync(bonusTag);
                }
            }
        }
    }
}
