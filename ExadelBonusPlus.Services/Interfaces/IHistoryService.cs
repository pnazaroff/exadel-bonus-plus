using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<History> AddHistory(History model);
        Task<History> DeleteHistory(Guid id);
        Task<IEnumerable<History>> GetAllHistory();
        Task<History> GetHistoryById(Guid id);
        Task<IEnumerable<History>> GetHistoryByUsageDate(DateTime usageDateStart, DateTime usegeDateEnd);
        Task<IEnumerable<History>> GetUserHistory(Guid userId );
        Task<IEnumerable<History>> GetBonusHistory(Guid bonusId );

    }
}
