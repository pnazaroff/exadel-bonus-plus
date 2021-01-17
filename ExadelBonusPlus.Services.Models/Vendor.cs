using System.Collections.Generic;

namespace ExadelBonusPlus.Services.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Info { get; set; }
        public List<Location> Locations { get; set; }
    }
}
