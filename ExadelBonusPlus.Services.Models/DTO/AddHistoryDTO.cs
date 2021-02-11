using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models
{
    public class AddHistoryDTO
    {
        public Guid UserId { get; set; }
        public Guid BonusId { get; set; }
        public DateTime DateUse { get; set; }
        public int Rating { get; set; }
    }
}
