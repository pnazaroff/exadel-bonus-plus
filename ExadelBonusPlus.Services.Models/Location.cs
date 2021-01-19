namespace ExadelBonusPlus.Services.Models
{
    public class Location
    {
        public string City { get; set; }

        public string Country { get; set; }

        public double[] Coordinates { get; set; }

        public Location()
        {
            Coordinates = new double[2];
        }
    }
}
