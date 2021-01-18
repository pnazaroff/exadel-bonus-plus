using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models.HistoryOfUserManagment
{
    /// <summary>
    /// Represents a History of using discounts by the user.
    /// </summary>
    class HistoryOfUser
    {
        /// <summary>
        /// Gets or sets identifier of history.
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// Gets or sets date of using discount.
        /// </summary>
        public DateTime dateOfAdd { get; set; }
        /// <summary>
        /// Gets or sets identifier of discount.
        /// </summary>
        public Guid idPromo { get; set; }
        /// <summary>
        /// Gets or sets identifier of user.
        /// </summary>
        public Guid idUser { get; set; }
        /// <summary>
        /// Gets or sets unique code.
        /// </summary>
        public string code { get; set; }
    }
}
