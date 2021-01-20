using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ExadelBonusPlus.Services.Models
{
    class Promotion
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Vendor { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int Estimate { get; set; }

        [BsonElement("Location")]
        public Location Location { get; set; }

        public List<String> Tags { get; set; }
    }
}
