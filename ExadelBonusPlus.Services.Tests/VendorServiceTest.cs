using AutoMapper;
using Bogus;
using ExadelBonusPlus.Services.Models;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ExadelBonusPlus.Services.Tests
{
    public class VendorServiceTest
    {
        private IMapper _mapper;
        private List<VendorDto> _fakeVendors;
        private List<AddVendorDto> _fakeAddVendors;
        private VendorService _vendorService;
        private IValidator<VendorDto> _vendorDtoValidator;
        private IVendorRepository _vendorRepository;
        private IValidator<AddVendorDto> _addVendorDtoValidator;

        public VendorServiceTest()
        {
            _fakeVendors = new List<VendorDto>();
            _fakeAddVendors = new List<AddVendorDto>();
        }
        [Fact]
        public async Task<List<VendorDto>> Vendor_FindAllVendorAsync_Return_ListVendorDTO()
        {
            CreateDefaultVendorServiceInstance();

            var vendorList = await _vendorService.GetAllVendorsAsync(default(CancellationToken));

            Xunit.Assert.NotNull(vendorList);
            return (List<VendorDto>)vendorList;
        }
        private void CreateDefaultVendorServiceInstance()
        {
            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            var vendorsGenerator = new Faker<VendorDto>()
                .RuleFor(v => v.Id, f => Guid.NewGuid())
                .RuleFor(v => v.Name, f => f.Company.CompanyName())
                .RuleFor(v => v.Email, (f, v) => f.Internet.Email(v.Name));


            var addVendorsGenerator = new Faker<AddVendorDto>()
                .RuleFor(v => v.Name, f => f.Company.CompanyName())
                .RuleFor(v => v.Email, (f, v) => f.Internet.Email(v.Name));

            for (int i = 0; i < 10; i++)
            {
                _fakeVendors.Add(vendorsGenerator.Generate());
                _fakeAddVendors.Add(addVendorsGenerator.Generate());
            }

            _vendorService = new VendorService(_vendorRepository, _mapper, _vendorDtoValidator, _addVendorDtoValidator);
         }
    }
}
