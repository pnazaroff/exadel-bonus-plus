using System;

namespace ExadelBonusPlus.Services.Models.DTO
{
    public class BonusHistoryDto
    {
        public UserInfoHistoryDto UserInfoDTO { get; set; }
        public DateTime UsageDate { get; set; }
        public int Rating { get; set; }
    }
}
