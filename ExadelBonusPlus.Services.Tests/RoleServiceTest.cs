using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;
using AutoMapper;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDbGenericRepository;
using Moq;
using SampleDataGenerator;
using Xunit;

namespace ExadelBonusPlus.Services.Tests
{
    public class RoleServiceTest
    {
        private Mock<RoleManager<ApplicationRole>> _roleManagerMock;
        private RoleManager<ApplicationRole> _roleManager;
        private RoleService _roleService;
        private IMapper _mapper;
        private List<ApplicationRole> _fakeRoles;
        private static Random _random;

        public RoleServiceTest()
        {
            
        }



        [Fact]
        public async Task<RoleDTO> AddRoleAsync_Return_RoleDto()
        {
            CreateDefaultApplicationRoleServiceInstance();
            var roleName = _fakeRoles[0].Name;
            var roleDto = await _roleService.AddRole(roleName);
            
            Assert.NotNull(roleDto);
            return roleDto;
        }
        [Fact]
        public async Task<RoleDTO> UpdateRoleAsync_Return_RoleDto()
        {
            CreateDefaultApplicationRoleServiceInstance();
            var roleName = _fakeRoles[0];
            var roleDto = await _roleService.UpdateRole(roleName.Id, RandomString(5));

            Assert.NotNull(roleDto);
            return roleDto;
        }
        [Fact]
        public async Task<RoleDTO> DeleteRoleAsync_Return_RoleDto()
        {
            CreateDefaultApplicationRoleServiceInstance();
            var roleDto = await _roleService.DeleteRole(_fakeRoles[0].Id);

            Assert.NotNull(roleDto);
            return roleDto;
        }

        Mock<RoleManager<ApplicationRole>> GetRoleManagerMock()
        {
            return new Mock<RoleManager<ApplicationRole>>(
                new Mock<IRoleStore<ApplicationRole>>().Object,
                new IRoleValidator<ApplicationRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<ApplicationRole>>>().Object);
        }
       
        private void CreateDefaultApplicationRoleServiceInstance()
        {
            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            _random = new Random();
            var historyGenerator = Generator
                .For<ApplicationRole>()
                .For(x => x.Id)
                .ChooseFrom(Guid.NewGuid())
                .For(x => x.Name)
                .ChooseFrom(RandomString(10));
            _fakeRoles = historyGenerator.Generate(10).ToList();
            _roleManagerMock = GetRoleManagerMock();
            _roleManagerMock.Setup(s => s.FindByIdAsync(It.IsAny<String>())).Returns(Task.FromResult(new ApplicationRole()));
            _roleManagerMock.Setup(s => s.FindByNameAsync(It.IsAny<String>())).Returns(Task.FromResult(new ApplicationRole()));


            _roleManagerMock.Setup(s => s.CreateAsync(It.IsAny<ApplicationRole>())).ReturnsAsync(IdentityResult.Success);
            _roleManagerMock.Setup(s => s.DeleteAsync(It.IsAny<ApplicationRole>())).ReturnsAsync(IdentityResult.Success);

            _roleManagerMock.Setup(s => s.GetRoleNameAsync(It.IsAny<ApplicationRole>())).ReturnsAsync(_fakeRoles[0].Name);
            _roleManagerMock.Setup(s => s.GetRoleIdAsync(It.IsAny<ApplicationRole>())).ReturnsAsync(_fakeRoles[0].Id.ToString);

            _roleManagerMock.Setup(s => s.GetClaimsAsync(It.IsAny<ApplicationRole>()));

            _roleManagerMock.Setup(s => s.AddClaimAsync(It.IsAny<ApplicationRole>(), It.IsAny<Claim>()));
            _roleManagerMock.Setup(s => s.NormalizeKey(RandomString(10)));
            _roleManagerMock.Setup(s => s.RemoveClaimAsync(It.IsAny<ApplicationRole>(), It.IsAny<Claim>())).ReturnsAsync(IdentityResult.Success);
            _roleManagerMock.Setup(s => s.RoleExistsAsync(RandomString(10)));
            _roleManagerMock.Setup(s => s.SetRoleNameAsync(It.IsAny<ApplicationRole>(),RandomString(10))).ReturnsAsync(IdentityResult.Success);



            _roleManagerMock.Setup(s => s.UpdateAsync(It.IsAny<ApplicationRole>())).ReturnsAsync(IdentityResult.Success);
            _roleManagerMock.Setup(s => s.UpdateNormalizedRoleNameAsync(It.IsAny<ApplicationRole>()));

            _roleManager = _roleManagerMock.Object;
            _roleService = new RoleService(_roleManager, _mapper);

            
            
        }
        
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}