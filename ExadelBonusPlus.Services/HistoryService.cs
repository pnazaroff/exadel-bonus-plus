using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepositry _historyRepositry;
        public HistoryService(IHistoryRepositry historyRepositry)
        {
            _historyRepositry = historyRepositry;

        }
        public Task<History> AddHistory(History model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            model.Id = Guid.NewGuid();
            _historyRepositry.AddAsync(model);
            var returnModel = _historyRepositry.GetByIdAsync(model.Id);
            return returnModel;
        }

        public async Task<History> DeleteHistory(Guid id)
        {
            var returnModel = await _historyRepositry.GetByIdAsync(id);
            if (returnModel is null)
            {
                throw new ArgumentNullException(nameof(returnModel));
            }

            await _historyRepositry.RemoveAsync(id);
            return returnModel;
        }

        public async Task<IEnumerable<History>> GetAllHistory()
        {
            return await _historyRepositry.GetAllAsync();
        }

        public async Task<History> GetHistoryById(Guid id)
        {
            return await _historyRepositry.GetByIdAsync(id);
        }

        public async Task<IEnumerable<History>> GetHistoryByUsageDate(DateTime usageDateStart, DateTime usegeDateEnd)
        {
            return await _historyRepositry.GetHistoryByUsageDate(usageDateStart, usegeDateEnd);
        }

        public async Task<IEnumerable<History>> GetUserHistory(Guid userId)
        {
            return await _historyRepositry.GetUserHistory(userId);
        }

        public async Task<IEnumerable<History>> GetBonusHistory(Guid bonusId)
        {
            return await _historyRepositry.GetBonusHistory(bonusId);
        }
    }
}
