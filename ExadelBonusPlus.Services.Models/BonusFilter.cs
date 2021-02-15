﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models
{
    //model for filter and sort bonuses
    public class BonusFilter
    {
        public FilterFields FilterBy { get; set; }
        public string SortBy { get; set; }
    }

    public class FilterFields
    {
        public bool? IsActive { get; set; }
        public List<string> Tags { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }
}