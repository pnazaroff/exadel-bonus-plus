using AutoMapper;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.DTO;
using ExadelBonusPlus.Services.Properties;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.Services
{
    public class VendorService : IVendorService
    {
        private IVendorRepository _vendorRepository;
        private readonly IMapper _mapper;

        public VendorService(IVendorRepository vendorRepository, IMapper mapper)
        {
            _vendorRepository = vendorRepository;
            _mapper = mapper;
        }
        public async Task<VendorDto> AddVendorAsync(AddVendorDto model, CancellationToken cancellationToken)
        {
            if (model is null)
            {
                throw new ArgumentNullException("", Resources.ModelIsNull);
            }
            var vendor = _mapper.Map<Vendor>(model);
            vendor.SetInitialValues(vendor);
            await _vendorRepository.AddAsync(vendor, cancellationToken);
            return _mapper.Map<VendorDto>(vendor);
         }

        public Task DeleteVendorAsync(Guid id, CancellationToken cancellationToken)
        {
            return _vendorRepository.RemoveAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<VendorDto>> GetAllVendorsAsync(CancellationToken cancellationToken)
        {
            var result = await _vendorRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<VendorDto>>(result);
        }

        public Task<Vendor> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _vendorRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<IEnumerable<Vendor>> SearchVendorByLocationAsync(string city, CancellationToken cancellationToken)
        {
            return _vendorRepository.SearchVendorByLocationAsync(city, cancellationToken);
        }

        public Task UpdateVendorAsync(Vendor model, CancellationToken cancellationToken)
        {
            return _vendorRepository.UpdateAsync(model.Id, model, cancellationToken);
        }
    }
}
