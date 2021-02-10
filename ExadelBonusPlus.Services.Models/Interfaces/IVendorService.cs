using ExadelBonusPlus.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IVendorService
    {
        Task<VendorDto> AddVendorAsync(VendorDto model, CancellationToken cancellationToken=default);

        Task<IEnumerable<Vendor>> GetAllVendorsAsync(CancellationToken cancellationToken = default);

        Task<Vendor> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken=default);

        Task UpdateVendorAsync(Vendor model, CancellationToken cancellationToken=default);

        Task DeleteVendorAsync(Guid id, CancellationToken cancellationToken=default);

        Task<IEnumerable<Vendor>> SearchVendorByLocationAsync(string city, CancellationToken cancellationToken=default);
    }
}
