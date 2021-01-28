using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ExadelBonusPlus.Services.Models
{
    public class BonusTag: IEntity<Guid>
    {

        public BonusTag()
        {
            CreatedDate = DateTime.Now;
        }

        [BsonId]
        [BsonElement("_id")]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatorId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid ModifierId { get; set; }

        public string Name { get; set; }

        public List<Bonus> BonusList { get; set; }
    }
}
