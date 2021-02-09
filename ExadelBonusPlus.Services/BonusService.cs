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

        public async Task<BonusDto> AddBonusAsync(AddBonusDto model, CancellationToken cancellationToken = default)
        {
            if (model is null)
            {
                throw new ArgumentNullException("", Resources.ModelIsNull);
            }
            var bonus = _mapper.Map<Bonus>(model);
            bonus.SetInitialValues(bonus);
            await _bonusRepository.AddAsync(bonus, cancellationToken);
            await AddTagInCollection(bonus, cancellationToken);
            return _mapper.Map<BonusDto>(bonus);
        }

        public async Task<List<BonusDto>> FindAllBonusAsync(CancellationToken cancellationToken)
        {
            var result = await _bonusRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<BonusDto>>(result);
        }
        
        public async Task<List<BonusDto>> FindAllActiveBonusAsync(CancellationToken cancellationToken)
        {
            var result = await _bonusRepository.GetAllActiveBonusAsync(cancellationToken);
            return _mapper.Map<List<BonusDto>>(result);
        }

        public async Task<BonusDto> FindBonusByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }
            var result = await _bonusRepository.GetByIdAsync(id, cancellationToken);
            return result is null ? throw new ArgumentException("", Resources.FindbyIdError) : _mapper.Map<BonusDto>(result);
        }

        public async Task<BonusDto> UpdateBonusAsync(Guid id, BonusDto model, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }
            if (model is null)
            {
                throw new ArgumentNullException("", Resources.ModelIsNull);
            }
            var bonus = _mapper.Map<Bonus>(model);

            await _bonusRepository.UpdateAsync(id, bonus, cancellationToken);
            await AddTagInCollection(bonus, cancellationToken);

            return model;
        }

        public async Task<BonusDto> DeleteBonusAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }

            var result =await _bonusRepository.RemoveAsync(id, cancellationToken);

            return _mapper.Map<BonusDto>(result);
        }

        public async Task<BonusDto> ActivateBonusAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }

            var result =  await _bonusRepository.ActivateBonusAsync(id, cancellationToken);

            return _mapper.Map<BonusDto>(result);
        }

        public async Task<BonusDto> DeactivateBonusAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }

            var result = await _bonusRepository.DeactivateBonusAsync(id, cancellationToken);

            return _mapper.Map<BonusDto>(result);
        }

        private async Task AddTagInCollection(Bonus bonus, CancellationToken cancellationToken)
        {
            foreach (string stringBonusTag in bonus.Tags)
            {
                BonusTag bonusTag;
                try // try, becouse mongo throw exception if 
                {
                    bonusTag = await _bonusTagRepository.FindTagByNameAsync(stringBonusTag, cancellationToken);
                    bonusTag.BonusIdList.Add(bonus.Id);
                    await _bonusTagRepository.UpdateAsync(bonusTag.Id, bonusTag, cancellationToken);
                }
                catch(InvalidOperationException e) //if bonusTag didn't find
                {
                    bonusTag = new BonusTag() {Name = stringBonusTag};
                    bonusTag.BonusIdList = new List<Guid>() { bonus.Id };
                    await _bonusTagRepository.AddAsync(bonusTag, cancellationToken);
                }
            }
        }
    }
}
