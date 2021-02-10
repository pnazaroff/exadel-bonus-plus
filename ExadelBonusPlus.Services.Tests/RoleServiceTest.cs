using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using SampleDataGenerator;
using Xunit;

namespace ExadelBonusPlus.Services.Tests
{
    public class RoleServiceTest
    {
        private Mock<RoleManager<ApplicationRole>> _roleMock;
        private RoleManager<ApplicationRole> _roleManager;
        private RoleService _roleService;
        private IMapper _mapper;
        private List<ApplicationRole> _fakeRoles;

        public RoleServiceTest(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }



        [Fact]
        public async Task<RoleDTO> Role_AddRoleAsync_Return_RoleDto()
        {
            CreateDefaultRoleServiceInstance();
            var roleDTO = _mapper.Map<ApplicationRole>(_fakeRoles[0]);

            var role = await _roleService.AddRole(roleDTO.Name);

            Assert.NotNull(role);
            return role;
        }

        private void CreateDefaultRoleServiceInstance()
        {
            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
            var roleGenerator = Generator.For<ApplicationRole>()
                .For(x => x.Name)
                .ChooseFrom(StaticData.LoremIpsum)
                .For(x => x.Id)
                .ChooseFrom(Guid.NewGuid());

            _fakeRoles = roleGenerator.Generate(10).ToList();


            _roleService = new RoleService(_roleManager, _mapper);
        }
    }
}