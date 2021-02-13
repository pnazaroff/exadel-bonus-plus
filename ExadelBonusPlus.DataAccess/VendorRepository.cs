using ExadelBonusPlus.Services.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace ExadelBonusPlus.DataAccess
{
    public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {

        }
        public async Task<List<Vendor>> SearchVendorByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await GetCollection().AsQueryable().Where(v=> v.Name.ToLower().Contains(name.ToLower())).ToListAsync(cancellationToken);
        }

    }
}
