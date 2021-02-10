using System;
using MongoDbGenericRepository.Attributes;

namespace ExadelBonusPlus.Services.Models
{
   
    public class TokenRefresh:IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

        public DateTime CreatedDate { get ; set; }
        public Guid CreatorId { get ; set ; }
        public DateTime? ModifiedDate { get; set; }
        public Guid ModifierId { get; set; }
        public bool IsDeleted { get ; set ; }
    }
}
