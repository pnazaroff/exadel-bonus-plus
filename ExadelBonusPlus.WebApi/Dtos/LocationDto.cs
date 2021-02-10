﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelBonusPlus.WebApi.Dtos
{
    public class LocationDto
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
