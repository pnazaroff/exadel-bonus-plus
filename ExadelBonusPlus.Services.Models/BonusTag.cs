using ExadelBonusPlus.Services.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models
{
    public class BonusTag: IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatorId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid ModifierId { get; set; }

        public string Name { get; set; }

        public List<Guid> BonusIdList { get; set; }

        public bool IsDeleted { get; set; }
    }
}
