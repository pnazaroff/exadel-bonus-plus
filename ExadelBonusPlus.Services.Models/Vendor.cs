using ExadelBonusPlus.Services.Models.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models
{
    public class Vendor : IEntity<Guid>
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        [BsonElement("Location")]
        public Location Location { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate {get;set;}
        public Guid CreatorId {get;set;}
        public DateTime? ModifiedDate {get;set;}
        public Guid ModifierId {get;set;}
    }
}
