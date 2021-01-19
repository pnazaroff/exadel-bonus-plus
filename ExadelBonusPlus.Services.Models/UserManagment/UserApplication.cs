using Microsoft.AspNetCore.Identity;

namespace ExadelBonusPlus.Services.Models.UserManagment
{
    /// <summary>
    /// Represents a User.
    /// </summary>
    class UserApplication : IdentityUser
    {

        /// <summary>
        /// Gets or sets in activ worker now.
        /// </summary>
        public bool IsActiv { get; set; }
        
    }
}
