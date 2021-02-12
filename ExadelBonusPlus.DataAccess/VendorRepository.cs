using ExadelBonusPlus.Services.Models;
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
        //should implement 
        public Task<List<Vendor>> SearchVendorByNameAsync(string name, CancellationToken cancellationToken)
        {
            return GetCollection().Find(Builders<Vendor>.Filter.Eq(new ExpressionFieldDefinition<Vendor, string>(x => x.Name), name)
                                        & Builders<Vendor>.Filter.Eq(new ExpressionFieldDefinition<Vendor, bool>(x => x.IsDeleted), false)).ToListAsync(cancellationToken);
        }

    }
}
