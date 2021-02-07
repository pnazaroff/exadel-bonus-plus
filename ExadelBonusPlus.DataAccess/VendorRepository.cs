using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.Interfaces;
using Microsoft.Extensions.Options;
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

        public async Task<Vendor> SearchVendorByLocation(string city, CancellationToken cancellationToken)
        {
            var cityFilter = Builders<Vendor>.Filter.Eq(vendor => vendor.Location.City, city);
            var deletionFilter = Builders<Vendor>.Filter.Eq(vendor => vendor.IsDeleted, false);
            var vendor = await GetCollection().Find(cityFilter&deletionFilter).FirstAsync(cancellationToken);

            return vendor;
        }
        

    }
}
