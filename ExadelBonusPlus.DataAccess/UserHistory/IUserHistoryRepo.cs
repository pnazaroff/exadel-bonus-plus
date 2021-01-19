using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models.UserHistoryManager
{
    public interface IUserHistoryRepo
    {
        IEnumerable<UserHistory> GetUserHistory(Guid Id);
        IEnumerable<UserHistory> GetAllUserHistory();
        IEnumerable<UserHistory> GetPromoHistory(Guid PromoId);
        UserHistory getUserHistoryById(Guid Id);
        UserHistory AddUserHistory(UserHistory userHistory);
        UserHistory UpdateUserHistory(UserHistory userHistoryUpdate);
        UserHistory DeleteUserHistory(Guid Id);
    }
}
