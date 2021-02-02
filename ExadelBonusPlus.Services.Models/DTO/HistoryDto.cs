using System;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.WebApi.Controllers
{
    public class HistoryDto
    {
        public Guid id { get; set; }
        public UserInfo UserInfo { get; set; }
        public string BonusName { get; set; }
        public string PromeCode { get; set; }
        public DateTime UsegeDate { get; set; }
    }
}