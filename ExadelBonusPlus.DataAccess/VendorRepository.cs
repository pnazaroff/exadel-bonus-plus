using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.DataAccess
{
    public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {

        }
        //should implement 
        public async Task<List<Vendor>> SearchVendorByNameAsync(string name, CancellationToken cancellationToken)
        {
            var collection = GetCollection();
            var indexKeysDefinition = Builders<Vendor>.IndexKeys.Ascending(vendor => vendor.Name);
            var indexName = await collection.Indexes.CreateOneAsync(new CreateIndexModel<Vendor>(indexKeysDefinition));

            var textFilter = Builders<Vendor>.Filter.Text(name);
            var deletionFilter =
                Builders<Vendor>.Filter.Eq(new ExpressionFieldDefinition<Vendor, bool>(x => x.IsDeleted), false);
        
        }

    }
}
