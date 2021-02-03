using System;
using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models
{
    public class AddBonusDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Guid Company { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public List<Location> Locations { get; set; }

        public string[] Tags { get; set; }
    }
}
