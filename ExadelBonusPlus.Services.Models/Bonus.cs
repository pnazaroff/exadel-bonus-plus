using System;
using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models
{
    public class Bonus: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid ModifierId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Phone { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public double Rating { get; set; }
        public List<Location> Locations { get; set; }
        public List<string> Tags { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}
