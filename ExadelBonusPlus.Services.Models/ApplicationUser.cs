using System;
using System.Collections.Generic;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace ExadelBonusPlus.Services.Models
{
    
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public ApplicationUser() : base()
        {
        }

        public ApplicationUser(string userName, string email) : base(userName, email)
        {
            IsActive = true;
        }
        /// <summary>
        /// Gets or sets in activ worker now.
        /// </summary>
        public bool IsActive { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
