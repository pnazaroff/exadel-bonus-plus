using System;
using System.Collections.Generic;
using System.Text;
using ExadelBonusPlus.Services.Models;

namespace ExadelBonusPlus.Services
{
    public static class EntityExtensions
    {
        public static void SetInitialValues(this IEntity<Guid> entityClass, IEntity<Guid> model)
        {
            model.CreatedDate = DateTime.Now;

            if(model.GetType() == typeof(Bonus))
                (model as Bonus).IsActive = true;
        }
    }
}
