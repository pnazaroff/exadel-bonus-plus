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

        Task<VendorDto> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken=default);

        Task<VendorDto> UpdateVendorAsync(Guid id,VendorDto model, CancellationToken cancellationToken=default);

        Task<VendorDto> DeleteVendorAsync(Guid id, CancellationToken cancellationToken=default);

        Task<List<VendorDto>> SearchVendorByNameAsync(string name, CancellationToken cancellationToken=default);
    }
}
