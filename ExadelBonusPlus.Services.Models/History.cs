﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models
{
    public class History: IEntity<Guid>
    {
        public History()
        {
            CreatedDate = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid ModifierId { get; set; }




        public Guid UserId { get; set; }
        public Guid BonusId { get; set; }

        public string PromoCode { get; set; }
    }
}
