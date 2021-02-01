using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.DataAccess
{
    public class VendorRepository : BaseRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(IOptions<MongoDbSettings> mongoDbSettings) : base(mongoDbSettings)
        {

        }
    }
}
