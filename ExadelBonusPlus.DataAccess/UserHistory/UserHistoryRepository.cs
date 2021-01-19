using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models.UserHistoryManager;
using MongoDB.Driver;

namespace ExadelBonusPlus.DataAccess.UserHistory
{
    public class UserHistoryRepository:BaseRepository<Services.Models.UserHistoryManager.UserHistory>, IUserHistoryRepository
    {
        public UserHistoryRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Services.Models.UserHistoryManager.UserHistory>> GetUserHistory(Guid id)
        {
            var UserHistory = await _dbSet.FindAsync(Builders<Services.Models.UserHistoryManager.UserHistory>.Filter.Eq("UserId", id));
            return (IEnumerable<Services.Models.UserHistoryManager.UserHistory>)UserHistory;
        }
    }
}
