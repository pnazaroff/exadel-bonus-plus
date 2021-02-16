using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SampleDataGenerator;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;
using Assert = Xunit.Assert;

namespace ExadelBonusPlus.Services.Tests
{
    public class BonusServiceTest
    {
        private Mock<IBonusRepository> _bonusRep;
        private IBonusRepository _mockBonusRep;
        private BonusService _bonusService;
        private Mock<IVendorRepository> _vendorRep;
        private IVendorRepository _mockVendorRep;
        private VendorService _vendorService;
        private IMapper _mapper;
        private List<BonusDto> _fakeBonuseDtos;

        public BonusServiceTest()
        {
            
        }

        public void Startup()
        {

        }

       [Fact]
        public async Task Bonus_AddBonusAsync_Return_BonusDto()
        {
            CreateDefaultBonusServiceInstance();
            var bonusDto = _mapper.Map<AddBonusDto>(_fakeBonuseDtos[0]);

            var bonus = await _bonusService.AddBonusAsync(bonusDto, default(CancellationToken));

            Assert.NotNull(bonus);
        }

        [Fact]
        public async Task Bonus_FindAllBonusAsync_Return_ListBonusDTO()
        {
            CreateDefaultBonusServiceInstance();

            var bonusList = await _bonusService.FindAllBonusesAsync(default(CancellationToken));

            Assert.NotNull(bonusList);
        }

        [Fact]
        public async Task Bonus_FindBonusesAsync_Return_ListBonusDTO()
        {
            CreateDefaultBonusServiceInstance();

            var bonusList = await _bonusService.FindBonusesAsync(new BonusFilter(), default(CancellationToken));

            Assert.NotNull(bonusList);
        }

        [Fact]
        public async Task Bonus_FindBonusByIdAsync_Return_BonusDTO()
        {
            CreateDefaultBonusServiceInstance();
            var idBonus = _fakeBonuseDtos[0].Id;

            var bonus = await _bonusService.FindBonusByIdAsync(idBonus, default(CancellationToken));

            Assert.NotNull(bonus);
        }

        [Fact]
        public async Task Bonus_UpdateBonusAsync_Return_BonusDTO()
        {
            CreateDefaultBonusServiceInstance();
            var idBonus = _fakeBonuseDtos[0].Id;
            var bonusDto = _mapper.Map<UpdateBonusDto>(_fakeBonuseDtos[0]);

            var bonus = await _bonusService.UpdateBonusAsync(idBonus, bonusDto, default(CancellationToken));

            Assert.NotNull(bonus);
        }

        [Fact]
        public async Task Bonus_DeleteBonusAsync_Return_BonusDTO()
        {
            CreateDefaultBonusServiceInstance();
            var idBonus = _fakeBonuseDtos[0].Id;

            var bonus = await _bonusService.DeleteBonusAsync(idBonus, default(CancellationToken));

            Assert.NotNull(bonus);
        }

        [Fact]
        public async Task Bonus_ActivateBonusAsync_Return_BonusDTO()
        {
            CreateDefaultBonusServiceInstance();
            var idBonus = _fakeBonuseDtos[0].Id;

            var bonus = await _bonusService.ActivateBonusAsync(idBonus, default(CancellationToken));

            Assert.True(bonus.IsActive);
        }

        [Fact]
        public async Task Bonus_DeactivateBonusAsync_Return_BonusDTO()
        {
            CreateDefaultBonusServiceInstance();
            var idBonus = _fakeBonuseDtos[0].Id;

            var bonus = await _bonusService.DeactivateBonusAsync(idBonus, default(CancellationToken));

            Assert.False(bonus.IsActive);
        }

        [Fact]
        public async Task Bonus_UpdateBonusRatingAsync_Return_BonusDTO()
        {
            CreateDefaultBonusServiceInstance();
            var idBonus = _fakeBonuseDtos[0].Id;
            double rating = 4.55;

            var bonus = await _bonusService.UpdateBonusRatingAsync(idBonus, rating, default(CancellationToken));

            Assert.True(bonus.Rating > 0);
        }

        [Fact]
        public async Task Bonus_GetBonusTagsAsync_Return_ListString()
        {
            CreateDefaultBonusServiceInstance();
            
            var tags = await _bonusService.GetBonusTagsAsync(default(CancellationToken));

            Assert.True(tags.Count()>0);
        }

        private void CreateDefaultBonusServiceInstance()
        {
            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            var bonusGenerator = Generator
                    .For<BonusDto>()
                    .For(x => x.Id)
                    .ChooseFrom(Guid.NewGuid())
                    .For(x => x.Title)
                    .ChooseFrom(StaticData.LoremIpsum)
                    .For(x => x.Description)
                    .ChooseFrom(StaticData.LoremIpsum)
                    .For(x => x.DateStart)
                    .ChooseFrom(DateTime.Now)
                    .For(x => x.DateEnd)
                    .ChooseFrom(DateTime.Now)
                    .For(x => x.IsActive)
                    .ChooseFrom(true)
                ;

            _fakeBonuseDtos = bonusGenerator.Generate(10).ToList();

            _bonusRep = new Mock<IBonusRepository>();
            _bonusRep.Setup(s => s.AddAsync(It.IsAny<Bonus>(), default(CancellationToken)));
            _bonusRep.Setup(s => s.GetAllAsync(default(CancellationToken))).ReturnsAsync(_mapper.Map<List<Bonus>>(_fakeBonuseDtos));
            _bonusRep.Setup(s => s.GetBonusesAsync(It.IsAny<BonusFilter>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<List<Bonus>>(_fakeBonuseDtos));
            _bonusRep.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<Bonus>(_fakeBonuseDtos[0]));
            _bonusRep.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Bonus>(), default(CancellationToken)));
            _bonusRep.Setup(s => s.RemoveAsync(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<Bonus>(_fakeBonuseDtos[0]));
            _bonusRep.Setup(s => s.ActivateBonusAsync(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<Bonus>(_fakeBonuseDtos[0]));
            _fakeBonuseDtos[9].IsActive = false;
            _bonusRep.Setup(s => s.DeactivateBonusAsync(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<Bonus>(_fakeBonuseDtos[9]));
            _fakeBonuseDtos[0].Rating = 3.00;
            _bonusRep.Setup(s => s.UpdateBonusRatingAsync(It.IsAny<Guid>(), It.IsAny<double>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<Bonus>(_fakeBonuseDtos[0]));
            _bonusRep.Setup(s => s.GetBonusTagsAsync(default(CancellationToken))).ReturnsAsync(new List<string>(){"Pizza","Coffee"});

            _mockBonusRep = _bonusRep.Object;

            _bonusService = new BonusService(_mockBonusRep, _mapper);

            _vendorRep = new Mock<IVendorRepository>();
            _vendorRep.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<Vendor>(new Vendor()));
            _mockVendorRep = _vendorRep.Object;
            _vendorService = new VendorService(_mockVendorRep, _mapper);

            var bonusResolver = new BonusResolver(_vendorService, _mapper);

            //var services = new ServiceCollection();
            //services.AddSingleton<BonusResolver>(bonusResolver);

            //var startup = new TestingStartup(default);
            //startup.ConfigureServices(services);
            //var provider = services.BuildServiceProvider();
        }
    }

    public partial class Startup
    {
        public Startup()
        {
            var a = 1;
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var _mapper = new Mapper(configuration);

            var _vendorRep = new Mock<IVendorRepository>();
            _vendorRep.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<Vendor>(new Vendor()));
            var _mockVendorRep = _vendorRep.Object;

            var _vendorService = new VendorService(_mockVendorRep, _mapper);
            services.AddSingleton<IVendorService>(_vendorService);
        }
    }
}
