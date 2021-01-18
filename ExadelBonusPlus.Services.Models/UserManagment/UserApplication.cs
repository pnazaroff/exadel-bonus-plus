using Microsoft.AspNetCore.Identity;

namespace ExadelBonusPlus.Services.Models.UserManagment
{
    /// <summary>
    /// Represents a User.
    /// </summary>
    class UserApplication : IdentityUser
    {
        /// <summary>
        /// Gets or sets user's gender.
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// Gets or sets user's lastname.
        /// </summary>
        public string family_name { get; set; }
        /// <summary>
        /// Gets or sets user's firstname.
        /// </summary>
        public string given_name { get; set; }
        /// <summary>
        /// Gets or sets user's middlename.
        /// </summary>
        public string middle_name { get; set; }
        /// <summary>
        /// Gets or sets in activ worker now.
        /// </summary>
        public bool isActiv { get; set; }
        
    }
}
