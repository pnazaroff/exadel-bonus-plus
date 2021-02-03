using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models.Interfaces
{
    public interface IVendorRepository : IRepository<Vendor, Guid>
    {
        Task<Vendor> SearchVendorByLocation(Location location, CancellationToken cancellationToken);
    }
}
