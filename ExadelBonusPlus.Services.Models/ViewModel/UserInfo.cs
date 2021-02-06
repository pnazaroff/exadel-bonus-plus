using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models.ViewModel
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
    }
}
