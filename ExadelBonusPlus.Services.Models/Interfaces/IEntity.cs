namespace ExadelBonusPlus.Services.Models
{
    using System;

    /// <summary>
    /// Interface IEntity.
    /// </summary>
    public interface IEntity<TId>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public TId Id { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the creator identifier.
        /// </summary>
        /// <value>The creator identifier.</value>
        public TId CreatorId { get; set; }
        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>The modified date.</value>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Gets or sets the modifier identifier.
        /// </summary>
        /// <value>The modifier identifier.</value>
        public TId ModifierId { get; set; }
        /// <summary>
        /// Gets or sets the IsDeleted.
        /// </summary>
        /// <value>The modifier IsDeleted.</value>
        public bool IsDeleted { get; set; }
    }
}