using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IVendorRepository : IRepository<Vendor, Guid>
    {
        Task<IEnumerable<Vendor>> SearchVendorByLocationAsync(string  city, CancellationToken cancellationToken);
    }
}
