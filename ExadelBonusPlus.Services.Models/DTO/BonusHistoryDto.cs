using System;

namespace ExadelBonusPlus.Services.Models
{
    public class BonusHistoryDto
    {
        public UserInfoHistoryDto UserInfo { get; set; }
        public DateTime UsageDate { get; set; }
        public int Rating { get; set; }
    }
}
