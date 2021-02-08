using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
