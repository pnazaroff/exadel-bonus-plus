using System;
using AutoMapper;
using ExadelBonusPlus.Services.Models.DTO;

namespace ExadelBonusPlus.Services.Models
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<BonusDto, Bonus>().ReverseMap();
            CreateMap<AddBonusDto, Bonus>().AfterMap((src, dest) => dest.Id = Guid.NewGuid());

            //Only for tests
            CreateMap<BonusDto, AddBonusDto>(); 
            
            CreateMap<ApplicationUser, UserInfoDTO > ()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom<UserResolver>())
                .ReverseMap();

            CreateMap<ApplicationRole, RoleDTO>().ReverseMap();

            //From model to dto
            CreateMap<Vendor, VendorDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<AddVendorDto, Vendor>()
                .AfterMap((src, dest) => dest.Id = Guid.NewGuid());

            
        }
    }
}
