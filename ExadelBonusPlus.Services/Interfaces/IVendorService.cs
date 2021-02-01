using ExadelBonusPlus.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Interfaces
{
    public interface IVendorService
    {
        Task AddVendorAsync(Vendor model);

        Task<IEnumerable<Vendor>> GetAllVendorsAsync();

        Task<Vendor> GetVendorByIdAsync(Guid id);

        Task UpdateVendorAsync(Vendor model);

        Task DeleteVendorAsync(Guid id);
    }
}
