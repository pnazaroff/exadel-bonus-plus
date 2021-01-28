using System;
using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models.DTO
{
    public class BonusDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid Vendor { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int Estimate { get; set; }

        public Location Location { get; set; }

    }
}
