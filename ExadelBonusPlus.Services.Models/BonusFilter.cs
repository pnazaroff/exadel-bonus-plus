using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models
{
    //model for filter and sort bonuses
    public class BonusFilter
    {
        public bool? IsActive { get; set; }
        public List<string> Tags { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        public Guid CompanyId { get; set; }
        public int LastCount { get; set; }
        public string SortBy { get; set; }
    }
}
