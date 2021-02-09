using System;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.WebApi.Controllers
{
    public class HistoryDto
    {
        public Guid id { get; set; }
        public UserInfoDTO UserInfo { get; set; }
        public BonusDto Bonus { get; set; }
        public DateTime UsegeDate { get; set; }
    }
}