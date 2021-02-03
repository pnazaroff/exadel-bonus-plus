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

        public Task<Vendor> SearchVendorByLocation(Location location, CancellationToken cancellationToken)
        {
            var vendor = GetCollection().Find(Builders<Vendor>.Filter.Eq("location", location)).FirstAsync(cancellationToken);
            return vendor;
        }
        

    }
}
