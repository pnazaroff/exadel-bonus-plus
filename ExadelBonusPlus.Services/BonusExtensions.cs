using System;
using System.Collections.Generic;
using System.Text;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services
{
    public static class BonusExtensions
    {
        public static void SetInitialValues(this Bonus bonusClass, Bonus bonus)
        {
            bonus.CreatedDate = DateTime.Now;
        }
    }
}
