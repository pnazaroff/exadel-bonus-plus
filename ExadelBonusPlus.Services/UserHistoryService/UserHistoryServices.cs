using ExadelBonusPlus.Services.Models.UserHistoryManager;
using System;
using System.Collections.Generic;
using System.Text;
using ExadelBonusPlus.DataAccess.UserHistory;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.UserHistoryService
{
    public class UserHistoryServices 
    {
        private readonly IUserHistoryRepository _UserHistoryRepositoryRepository;
        public UserHistoryServices(IUserHistoryRepository UserHistoryRepositoryRepository)
        {
            _UserHistoryRepositoryRepository = UserHistoryRepositoryRepository;
        }

        public void Add(UserHistory obj)
        {
            _UserHistoryRepositoryRepository.Add(obj);
        }
        
        public async Task<IEnumerable<UserHistory>> GetAll()
        {
           return  await _UserHistoryRepositoryRepository.GetAll();
        }

        public async Task<UserHistory> GetById(Guid Id)
        {
            return await _UserHistoryRepositoryRepository.GetById(Id);
        }

        public void Remove(Guid id)
        {
            _UserHistoryRepositoryRepository.Remove(id);
        }

        public async  Task<IEnumerable<UserHistory>> GetUserHistory(Guid UserId)
        {
            return await _UserHistoryRepositoryRepository.GetUserHistory(UserId);
        }
        public void Update(Guid id, UserHistory obj)
        {
            _UserHistoryRepositoryRepository.Update(id, obj);
        }
    }
}
