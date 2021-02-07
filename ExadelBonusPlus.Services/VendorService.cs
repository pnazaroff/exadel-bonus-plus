using ExadelBonusPlus.Services.Interfaces;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
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
        public Task AddVendorAsync(Vendor model, CancellationToken cancellationToken)
        {
            return _vendorRepository.AddAsync(model, cancellationToken);
        }

        public Task DeleteVendorAsync(Guid id, CancellationToken cancellationToken)
        {
            return _vendorRepository.DeleteAsync(id, cancellationToken);
        }

        public Task<IEnumerable<Vendor>> GetAllVendorsAsync(CancellationToken cancellationToken)
        {
            return _vendorRepository.GetAllAsync(cancellationToken);
        }

        public Task<Vendor> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _vendorRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<Vendor> SearchVendorByLocation(string city, CancellationToken cancellationToken)
        {
            return _vendorRepository.SearchVendorByLocation(city, cancellationToken);
        }

        public Task UpdateVendorAsync(Vendor model, CancellationToken cancellationToken)
        {
            return _vendorRepository.UpdateAsync(model.Id, model, cancellationToken);
        }
    }
}
