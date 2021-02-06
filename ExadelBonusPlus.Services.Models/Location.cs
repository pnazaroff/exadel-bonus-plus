using ExadelBonusPlus.Services.Models.Interfaces;
using System;

namespace ExadelBonusPlus.Services.Models
{
    public class Location
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
