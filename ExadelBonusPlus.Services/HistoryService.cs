using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Properties;

namespace ExadelBonusPlus.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;
        public HistoryService(IHistoryRepository historyRepository,
                            IMapper mapper)
        {
            _historyRepository = historyRepository;
            _mapper = mapper;

        }
        public async Task<HistoryDto> AddHistory(AddHistoryDTO model, CancellationToken cancellationToken = default)
        {
            if (model is null)
            {
                throw new ArgumentNullException("", Resources.ModelIsNull);
            }
            var history = _mapper.Map<History>(model);
             await _historyRepository.AddAsync(history, cancellationToken);
            return _mapper.Map<HistoryDto>(history);
        }

        public async Task<HistoryDto> DeleteHistory(Guid id,  CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }
            var returnModel = await _historyRepository.GetByIdAsync(id);
            if (returnModel is null)
            {
                throw new ArgumentNullException("", Resources.DeleteError);
            }

            await _historyRepository.RemoveAsync(id, cancellationToken);
            var history = await _historyRepository.GetByIdAsync(id);

            return !(history is null) ? _mapper.Map<HistoryDto>(history) : throw new ArgumentNullException("", Resources.DeleteError);
        }

        public async Task<IEnumerable<HistoryDto>> GetAllHistory(CancellationToken cancellationToken = default)
        {
             var history = await _historyRepository.GetAllAsync(cancellationToken);
            return history is null ? throw new ArgumentNullException("", Resources.FindError) : _mapper.Map<List<HistoryDto>>(history);
        }

        public async Task<HistoryDto> GetHistoryById(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }
            var history = await  _historyRepository.GetByIdAsync(id,cancellationToken);
            return !(history is null) ? _mapper.Map<HistoryDto>(history) : throw new ArgumentNullException("", Resources.FindbyIdError);
        }
        
        
        public async Task<IEnumerable<UserHistoryDto>> GetUserHistoryByUsageDate(Guid userId, DateTime usageDateStart, DateTime usegeDateEnd,  CancellationToken cancellationToken = default)
        {
            var history = await _historyRepository.GetUserHistoryByUsageDate(userId, usageDateStart, usegeDateEnd, cancellationToken);
            return !(history is null) ? _mapper.Map<List<UserHistoryDto>>(history) : throw new ArgumentNullException("", Resources.FindError);
        }

        public async Task<IEnumerable<BonusHistoryDto>> GetBonusHistoryByUsageDate(Guid bonusId, DateTime usageDateStart, DateTime usegeDateEnd, CancellationToken cancellationToken = default)
        {
            var history = await _historyRepository.GetBonusHistoryByUsageDate(bonusId, usageDateStart, usegeDateEnd, cancellationToken);
            return !(history is null) ? _mapper.Map<List<BonusHistoryDto>>(history) : throw new ArgumentNullException("", Resources.FindError);
        }

        public async Task<IEnumerable<UserHistoryDto>> GetUserAllHistory(Guid userId, CancellationToken cancellationToken = default)
        {
            var history = await _historyRepository.GetUserHistory(userId, cancellationToken);
            return !(history is null) ? _mapper.Map<List<UserHistoryDto>>(history) : throw new ArgumentNullException("", Resources.FindError);
        }

        public async Task<IEnumerable<BonusHistoryDto>> GetBonusAllHistory(Guid bonusId, CancellationToken cancellationToken = default)
        {
            var history = await _historyRepository.GetBonusHistory(bonusId, cancellationToken);
            return !(history is null) ? _mapper.Map<List<BonusHistoryDto>>(history) : throw new ArgumentNullException("", Resources.FindError);

        }

        public async Task<int> GetCountHistoryByBonusIdAsync(Guid bonusId, CancellationToken cancellationToken = default)
        {
            return await _historyRepository.GetCountHistoryByBonusIdAsync(bonusId, cancellationToken);
        }
    }
}
