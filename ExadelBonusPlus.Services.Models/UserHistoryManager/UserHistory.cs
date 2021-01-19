using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace ExadelBonusPlus.Services.Models.UserHistoryManager
{
    public class UserHistory
    {
        public UserHistory()
        {
            
        }

        public UserHistory(Guid id, DateTime dateAdded, Guid promoId, Guid userId, string code)
        {
            Id = id;
            DateAdded = dateAdded;
            PromoId = promoId;
            UserId = userId;
            Code = code;
        }
        /// <summary>
        /// Gets or sets identifier of history.
        /// </summary>
        [BsonId]
        [BsonElement("_id")]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets date of using discount.
        /// </summary>
        public DateTime DateAdded { get; set; }
        /// <summary>
        /// Gets or sets identifier of discount.
        /// </summary>
        
        public Guid PromoId { get; set; }
        /// <summary>
        /// Gets or sets identifier of user.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Gets or sets unique code.
        /// </summary>
        public string Code { get; set; }
    }
}
