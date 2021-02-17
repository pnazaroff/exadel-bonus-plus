using System;
using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models
{
    public class UpdateBonusDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public List<Location> Locations { get; set; }
        public string[] Tags { get; set; }
    }
}
