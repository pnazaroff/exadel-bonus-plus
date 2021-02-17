using System;

namespace ExadelBonusPlus.Services.Models
{
    public class Vendor : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate {get;set;}
        public Guid CreatorId {get;set;}
        public DateTime? ModifiedDate {get;set;}
        public Guid ModifierId {get;set;}
    }
}
