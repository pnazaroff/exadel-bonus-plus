﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelBonusPlus.WebApi.Dtos
{
    public class VendorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public LocationDto Location { get; set; }

    }
}
