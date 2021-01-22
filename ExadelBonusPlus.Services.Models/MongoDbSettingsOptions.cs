

namespace ExadelBonusPlus.Services.Models
{
    public class MongoDbSettingsOptions
    {
        public const string MongoDbSettings = "MongoDbSettings";
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
