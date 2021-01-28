using ExadelBonusPlus.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services.Interfaces
{
    public interface IVendorService
    {
        Task<Vendor> AddVendorAsync(Vendor model);

        Task<List<Vendor>> FindAllVendorsAsync();

        Task<Vendor> FindVendorByIdAsync(Guid id);

        Task<Vendor> UpdateVendorAsync(Vendor model);

        Task<Vendor> DeleteVendorAsync(Guid id);
    }
}
