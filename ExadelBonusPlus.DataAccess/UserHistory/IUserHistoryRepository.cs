using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelBonusPlus.DataAccess.UserHistory
{
    public interface IUserHistoryRepository:IRepository<Services.Models.UserHistoryManager.UserHistory> 
    {
        Task<IEnumerable<Services.Models.UserHistoryManager.UserHistory>> GetUserHistory(Guid id);
    }
}