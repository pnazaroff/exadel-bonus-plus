using ExadelBonusPlus.Services.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services
{
    public class VendorService
    {
        private readonly MongoCollectionBase<Vendor> _vendors;

        public VendorService(IExadelBonusDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);


            _vendors = (MongoCollectionBase<Vendor>)database.GetCollection<Vendor>(settings.VendorsCollectionName);
        }

        public async Task<List<Vendor>> GetVendors()
        {
            return await _vendors.Find(new BsonDocument()).ToListAsync();
        }

    }
}
