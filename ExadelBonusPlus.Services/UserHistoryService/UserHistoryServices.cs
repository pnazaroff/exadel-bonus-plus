using ExadelBonusPlus.Services.Models.UserHistoryManager;
using System;
using System.Collections.Generic;
using System.Text;
using ExadelBonusPlus.DataAccess.UserHistory;

namespace ExadelBonusPlus.Services.UserHistoryService
{
    public class UserHistoryServices
    {
        private readonly IUserHistoryRepo _userHistoryRepo;

        public UserHistoryServices()
        {
            _userHistoryRepo = new MockUserHistory();
        }
        public UserHistoryServices(IUserHistoryRepo UserHistoryRepo)
        {
            _userHistoryRepo = UserHistoryRepo;
        }

        public IEnumerable<UserHistory> GetAllUserHistory()
        {
           return _userHistoryRepo.GetAllUserHistory();
        }

        public UserHistory GetUserHistoryById(Guid Id)
        {
            return _userHistoryRepo.getUserHistoryById(Id);
        }
        public IEnumerable<UserHistory> GetUserHistory(Guid UserId)
        {
            return _userHistoryRepo.GetUserHistory(UserId);
        }
        public IEnumerable<UserHistory> GetPromoHistory(Guid PromoId)
        {
            return _userHistoryRepo.GetPromoHistory(PromoId);
        }

        public UserHistory DeleteUserHistory(Guid Id)
        {
            return _userHistoryRepo.DeleteUserHistory(Id);
        }
        public UserHistory UpdateUserHistory(UserHistory userHistory)
        {
            return _userHistoryRepo.UpdateUserHistory(userHistory);
        }
        public UserHistory AddUserHistory(UserHistory userHistory)
        {
            return _userHistoryRepo.AddUserHistory(userHistory);
        }
    }
}
