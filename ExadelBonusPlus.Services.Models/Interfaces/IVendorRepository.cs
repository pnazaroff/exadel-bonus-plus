using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models.Interfaces
{
    public interface IVendorRepository : IRepository<Vendor, Guid>
    {
        Task<IEnumerable<Vendor>> SearchVendorByLocationAsync(string  city, CancellationToken cancellationToken);
    }
}
