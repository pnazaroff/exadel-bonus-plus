using ExadelBonusPlus.Services.Interfaces;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services
{
    public class VendorService : IVendorService
    {
        private IVendorRepository _vendorRepository;
        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public Task AddVendorAsync(Vendor model)
        {
            return _vendorRepository.AddAsync(model);
        }

        public Task DeleteVendorAsync(Guid id)
        {
            return _vendorRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return _vendorRepository.GetAllAsync();
        }

        public Task<Vendor> GetVendorByIdAsync(Guid id)
        {
            return _vendorRepository.GetByIdAsync(id);
        }

        public Task UpdateVendorAsync(Vendor model)
        {
            return _vendorRepository.UpdateAsync(model.Id, model);
        }
    }
}
