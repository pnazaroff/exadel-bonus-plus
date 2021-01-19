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
                new Services.Models.UserHistoryManager.UserHistory(){ UserId = Guid.Parse("53dba9a9-f4be-419c-b2fd-135a60f1be02"), Code = "ExadelBonusPlus",
                                                                    DateAdded = DateTime.Now, Id = new Guid(), PromoId = Guid.Parse("fd68e93d-ea7b-4b75-9b5a-60890c06e5b2")},
                new Services.Models.UserHistoryManager.UserHistory(){ UserId =  Guid.Parse("53dba9a9-f4be-419c-b2fd-135a60f1be02"), Code = "ExadelBonusPlus1",
                                                                    DateAdded = DateTime.Now, Id = new Guid(), PromoId = Guid.Parse("6f0031fe-11fa-4b14-b979-e7c30ff67f9d")},
                new Services.Models.UserHistoryManager.UserHistory(){ UserId = Guid.Parse("dc631af3-55a9-4ff1-80b7-cce589a494ee"), Code = "ExadelBonusPlus2",
                                                                    DateAdded = DateTime.Now, Id = new Guid(), PromoId = Guid.Parse("fd68e93d-ea7b-4b75-9b5a-60890c06e5b2")},
                new Services.Models.UserHistoryManager.UserHistory(){ UserId = Guid.Parse("dc631af3-55a9-4ff1-80b7-cce589a494ee"), Code = "ExadelBonusPlus3",
                                                                    DateAdded = DateTime.Now, Id = new Guid(), PromoId = Guid.Parse("6f0031fe-11fa-4b14-b979-e7c30ff67f9d")},

            };
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
