using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace ExadelBonusPlus.WebApi.ViewModel
{
    public class AuthResponce
    {
        public string Email { get; set; }
        public bool IsAuth { get; set; }
        public string AccessToken { get; set; }
        public List<string> Role { get; set; }
        public string RefreshToken { get; set; }
    }
}
