namespace ExadelBonusPlus.Services.Models
{
    public interface IExadelBonusDbSettings
    {
        string VendorsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    public class ExadelBonusDbSettings : IExadelBonusDbSettings
    {
        public string VendorsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
