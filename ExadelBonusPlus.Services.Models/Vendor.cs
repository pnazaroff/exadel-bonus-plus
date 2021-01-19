using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models
{
    public class Vendor
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        [BsonElement("Location")]
        public List<Location> Locations { get; set; }
    }
}
