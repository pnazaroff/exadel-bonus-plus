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
            
            CreateMap<ApplicationUser, UserInfoDTO > ()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom<UserResolver>())
                .ReverseMap();

            CreateMap<ApplicationRole, RoleDTO>().ReverseMap();

            //From model to dto
            CreateMap<Vendor, VendorDto>()
                .ForMember(vd => vd.Location, opts => opts.MapFrom(v => v.Location));
            CreateMap<Location, LocationDto>();

            //From dto to model 
            CreateMap<VendorDto, Vendor>()
                .ForMember(v => v.Location, opts => opts.MapFrom(vd => new Location { City = vd.Location.City, Address = vd.Location.Address, Country = vd.Location.Country, Latitude = vd.Location.Latitude, Longitude = vd.Location.Longitude }));
        }
    }
}
