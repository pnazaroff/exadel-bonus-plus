using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExadelBonusPlus.Services.Models.UserHistoryManager;

namespace ExadelBonusPlus.DataAccess.UserHistory
{
    public class MockUserHistory : IUserHistoryRepo
    {
        private readonly List<Services.Models.UserHistoryManager.UserHistory> _userHistory;

        public MockUserHistory()
        {
            _userHistory = new List<Services.Models.UserHistoryManager.UserHistory>()
            {
                new Services.Models.UserHistoryManager.UserHistory(){ UserId = new Guid(), Code = "ExadelBonusPlus", DateAdded = DateTime.Now, Id = new Guid(), PromoId = new Guid()},
                new Services.Models.UserHistoryManager.UserHistory(){ UserId = new Guid(), Code = "ExadelBonusPlus1", DateAdded = DateTime.Now, Id = new Guid(), PromoId = new Guid()},
                new Services.Models.UserHistoryManager.UserHistory(){ UserId = new Guid(), Code = "ExadelBonusPlus2", DateAdded = DateTime.Now, Id = new Guid(), PromoId = new Guid()},
                new Services.Models.UserHistoryManager.UserHistory(){ UserId = new Guid(), Code = "ExadelBonusPlus3", DateAdded = DateTime.Now, Id = new Guid(), PromoId = new Guid()},

            };
        }

        public MockUserHistory(List<Services.Models.UserHistoryManager.UserHistory> userHistory = null)
        {
            if (userHistory != null)
            {
                _userHistory = userHistory;
            }
        }

        public Services.Models.UserHistoryManager.UserHistory AddUserHistory(Services.Models.UserHistoryManager.UserHistory userHistory)
        {
            
            _userHistory.Add(userHistory);
            return userHistory;
        }
        
        public Services.Models.UserHistoryManager.UserHistory DeleteUserHistory(Guid Id)
        {
            var userHistory = _userHistory.FirstOrDefault(e => e.Id == Id);
            if (userHistory != null)
            {
                _userHistory.Remove(userHistory);
            }

            return userHistory;
        }

        public IEnumerable<Services.Models.UserHistoryManager.UserHistory> GetAllUserHistory()
        {
            return _userHistory;
        }

        public IEnumerable<Services.Models.UserHistoryManager.UserHistory> GetPromoHistory(Guid PromoId)
        {
            return _userHistory.Where(e => e.PromoId == PromoId);
        }

        public IEnumerable<Services.Models.UserHistoryManager.UserHistory> GetUserHistory(Guid UserId)
        {
            return _userHistory.Where(e => e.UserId == UserId);
        }

        public Services.Models.UserHistoryManager.UserHistory getUserHistoryById(Guid Id)
        {
            return _userHistory.FirstOrDefault(e => e.Id == Id);
        }

        public Services.Models.UserHistoryManager.UserHistory UpdateUserHistory(Services.Models.UserHistoryManager.UserHistory userHistoryUpdate)
        {
            var userHistory = _userHistory.FirstOrDefault(e => e.Id == userHistoryUpdate.Id);
            if (userHistory != null)
            {
                userHistory.UserId = userHistoryUpdate.UserId;
                userHistory.Id = userHistoryUpdate.Id;
                userHistory.Code = userHistoryUpdate.Code;
                userHistory.DateAdded = userHistoryUpdate.DateAdded;
                userHistory.PromoId = userHistoryUpdate.PromoId;
            }

            return userHistory;
        }
    }
}
