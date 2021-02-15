using System;
using ExadelBonusPlus.Services.Models.DTO;

namespace ExadelBonusPlus.Services.Models
{
    public class HistoryDto
    {
        public Guid id { get; set; }
        public UserInfoHistoryDto UserInfo { get; set; }
        public BonusDto Bonus { get; set; }
        public DateTime UsegeDate { get; set; }
        public int Rating { get; set; }
    }
}