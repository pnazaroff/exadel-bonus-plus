using ExadelBonusPlus.Services.Interfaces;
using ExadelBonusPlus.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services
{
    public class VendorService : IVendorService
    {
        public Task<Vendor> AddVendorAsync(Vendor model)
        {
            throw new NotImplementedException();
        }

        public Task<Vendor> DeleteVendorAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Vendor>> FindAllVendorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Vendor> FindVendorByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Vendor> UpdateVendorAsync(Vendor model)
        {
            throw new NotImplementedException();
        }
    }
}
