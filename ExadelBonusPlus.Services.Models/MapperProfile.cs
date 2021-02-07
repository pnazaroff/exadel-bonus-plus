using System;
using AutoMapper;

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
            CreateMap<ApplicationUser, UserInfo > ()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom<CustomResolver>())
                .ReverseMap();
        }
    }
}
