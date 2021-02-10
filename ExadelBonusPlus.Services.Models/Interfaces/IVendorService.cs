using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Models
{
    public interface IVendorService
    {
        Task<VendorDto> AddVendorAsync(AddVendorDto model, CancellationToken cancellationToken=default);

        Task<IEnumerable<VendorDto>> GetAllVendorsAsync(CancellationToken cancellationToken = default);

        Task<Vendor> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken=default);

        Task UpdateVendorAsync(Vendor model, CancellationToken cancellationToken=default);

        Task DeleteVendorAsync(Guid id, CancellationToken cancellationToken=default);

        Task<IEnumerable<Vendor>> SearchVendorByLocationAsync(string city, CancellationToken cancellationToken=default);
    }
}
