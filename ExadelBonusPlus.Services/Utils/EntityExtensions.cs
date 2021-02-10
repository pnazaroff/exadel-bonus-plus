using System;
using System.Collections.Generic;
using System.Text;
using ExadelBonusPlus.Services.Models;


namespace ExadelBonusPlus.Services
{
    public static class EntityExtensions
    {
        public static void SetInitialValues(this IEntity<Guid> model)
        {
            model.CreatedDate = DateTime.Now;
        }

        public static void SetInitialValues(this Bonus model)
        {
            model.CreatedDate = DateTime.Now;
            model.IsActive = true;
        }
    }
}
