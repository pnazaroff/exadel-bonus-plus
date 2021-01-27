using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ExadelBonusPlus.Services.Models
{
    [CollectionName("RefreshToken")]
    public class RefreshToken : IEntity<Guid>
    {
        [BsonId]
        [BsonElement("_id")]
        public Guid Id { get; set; }
        [BsonElement]
        public DateTime Expires { get; set; }
        [BsonElement]
        public bool IsExpired => DateTime.UtcNow >= Expires;
        [BsonElement]
        public DateTime? Revoked { get; set; }
        [BsonElement]
        public bool IsActive => Revoked == null && !IsExpired;
        [BsonElement]
        public string Value { get; set; }
        [BsonElement]
        public DateTime CreatedDate { get ; set; }
        [BsonElement]
        public Guid CreatorId { get ; set ; }
        [BsonElement]
        public DateTime? ModifiedDate { get ; set ; }
        [BsonElement]
        public Guid ModifierId { get ; set; }
    }
}
