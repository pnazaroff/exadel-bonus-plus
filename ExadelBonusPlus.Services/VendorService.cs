using AutoMapper;
using ExadelBonusPlus.Services.Models;
using ExadelBonusPlus.Services.Models.DTOValidator;
using ExadelBonusPlus.Services.Properties;
using FluentValidation;
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
        private readonly IValidator<VendorDto> _vendorDtoValidator;
        private readonly IValidator<AddVendorDto> _addVendorDtoValidator;

        public VendorService(IVendorRepository vendorRepository, 
            IMapper mapper, 
            IValidator<VendorDto> vendorDtoValidator, 
            IValidator<AddVendorDto> addVendorDtoValidator)
        {
            _vendorRepository = vendorRepository;
            _mapper = mapper;
            _vendorDtoValidator = vendorDtoValidator;
            _addVendorDtoValidator = addVendorDtoValidator;
        }
        public async Task<VendorDto> AddVendorAsync(AddVendorDto model, CancellationToken cancellationToken)
        {
           var results = _addVendorDtoValidator.Validate(model);
            if (!results.IsValid)
            {
                throw new ArgumentException("", Resources.ValidationError);
            }
            var vendor = _mapper.Map<Vendor>(model);
            vendor.SetInitialValues();
            await _vendorRepository.AddAsync(vendor, cancellationToken);
            return _mapper.Map<VendorDto>(vendor);
        }

        public async Task<VendorDto> DeleteVendorAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }

            var result = await _vendorRepository.RemoveAsync(id, cancellationToken);

            return _mapper.Map<VendorDto>(result);
        }

        public async Task<IEnumerable<VendorDto>> GetAllVendorsAsync(CancellationToken cancellationToken)
        {
            var vendors = await _vendorRepository.GetAllAsync(cancellationToken);

            var vendorDtos = _mapper.Map<IEnumerable<VendorDto>>(vendors);

            return vendorDtos;
        }

        public async Task<VendorDto> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }
            var result = await _vendorRepository.GetByIdAsync(id, cancellationToken);
            return result is null ? throw new ArgumentException("", Resources.FindbyIdError) : _mapper.Map<VendorDto>(result);
        }

        public async Task<List<VendorDto>> SearchVendorByNameAsync(string name, CancellationToken cancellationToken)
        {
            var result = await _vendorRepository.SearchVendorByNameAsync(name, cancellationToken);
            return result is null ? throw new ArgumentException("", Resources.FindbyIdError) : _mapper.Map<List<VendorDto>>(result);
         }

        public async Task<VendorDto> UpdateVendorAsync(Guid id, VendorDto model, CancellationToken cancellationToken)
        {
            var validationResult = _vendorDtoValidator.Validate(model);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException("", Resources.ValidationError);
            }
            if(id == Guid.Empty)
            {
                throw new ArgumentNullException("", Resources.IdentifierIsNull);
            }
            
            var vendor = _mapper.Map<Vendor>(model);

            await _vendorRepository.UpdateAsync(id, vendor, cancellationToken);

            return model;
         }
    }
}
