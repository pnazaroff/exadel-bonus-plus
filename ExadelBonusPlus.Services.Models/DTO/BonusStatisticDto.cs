using System;
using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models
{
    public class BonusStatisticDto
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string CompanyName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Rating { get; set; }
        public bool IsActive { get; set; }
        public List<Location> Locations { get; set; }
        public int Visits { get; set; }
    }
}
