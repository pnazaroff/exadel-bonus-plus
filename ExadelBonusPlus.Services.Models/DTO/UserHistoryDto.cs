using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models.DTO
{
    public class UserHistoryDto
    {
        public BonusDto BonusDto { get; set; }
        public DateTime UsageDate { get; set; }
        public int Rating { get; set; }
        public bool IsRated { get; set; }
    }
}
