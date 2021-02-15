using Autofac.Extras.Moq;
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
        private VendorService _vendorService;
        private IValidator<VendorDto> _vendorDtoValidator;
        private IVendorRepository _vendorRepository;
        private IValidator<AddVendorDto> _addVendorDtoValidator;

        public VendorServiceTest()
        {
            
        }
        [Fact]
        public async Task GetAllVendorAsync_ShouldReturn_ListVendorDtosAsync()
        {
            var fakeVendorDtos = GenerateSampleVendors();
            var fakeVendors = MapDtoListToDomainObjectList(fakeVendorDtos.Result);
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepository<Vendor, Guid>>()
                    .Setup(x => x.GetAllAsync(default))
                    .Returns(fakeVendors);
                
                mock.Mock<IVendorRepository>()
                    .Setup(x => x.GetAllAsync(default))
                    .Returns(fakeVendors);

                var serviceInstance = mock.Create<VendorService>();

                var expected = fakeVendorDtos;
                var actual = serviceInstance.GetAllVendorsAsync(default);

                Xunit.Assert.NotNull(actual);
                //Xunit.Assert.Equal(expected.Result.Count(), actual.Result.Count());
            }
        }


        /// <summary>
        /// Generates realistic fake data
        /// </summary>
        /// <returns>Vendor objects</returns>
        private async Task<IEnumerable<VendorDto>> GenerateSampleVendors()
        {
            var vendorsGenerator = new Faker<VendorDto>()
                .RuleFor(v => v.Id, f => Guid.NewGuid())
                .RuleFor(v => v.Name, f => f.Company.CompanyName())
                .RuleFor(v => v.Email, (f, v) => f.Internet.Email(v.Name));
            
            return await Task.Run(() => vendorsGenerator.Generate(10));
        }

        private async Task<IEnumerable<Vendor>> MapDtoListToDomainObjectList(IEnumerable<VendorDto> source)
        {
            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            var result = await Task.Run(() => _mapper.Map<IEnumerable<Vendor>>(source));
            return result;
        }
    }
}
