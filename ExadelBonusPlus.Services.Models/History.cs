using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models
{
    public class History: IEntity<Guid>
    {
        public History()
        {
           
        }
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid ModifierId { get; set; }
        public bool IsDeleted { get; set; }
        public int Rating { get; set; }
        public Guid BonusId { get; set; }

    }
}
