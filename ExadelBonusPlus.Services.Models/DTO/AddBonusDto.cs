using System;

namespace ExadelBonusPlus.Services.Models
{
    public class AddBonusDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid Vendor { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int Estimate { get; set; }

        public Location Location { get; set; }

        public string[] Tags { get; set; }

    }
}
