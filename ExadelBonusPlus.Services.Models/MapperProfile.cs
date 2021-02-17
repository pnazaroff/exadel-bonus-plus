using System;
using AutoMapper;

namespace ExadelBonusPlus.Services.Models
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Bonus, BonusDto>()
                .ForMember(dest => dest.Company,
                    opt =>
                    {
                        opt.MapFrom<BonusResolver>();
                    })
                .ReverseMap();
            CreateMap<AddBonusDto, Bonus>().AfterMap((src, dest) => dest.Id = Guid.NewGuid());
            CreateMap<UpdateBonusDto, Bonus>().ReverseMap();
            CreateMap<UpdateBonusDto, BonusDto>().ReverseMap();
            CreateMap<Bonus, BonusStatisticDto>().ForMember(dest => dest.CompanyName,
                opt =>
                {
                    opt.MapFrom<BonusStatisticCompanyNameResolver>();
                }).ForMember(dest => dest.Visits,
                opt =>
                {
                    opt.MapFrom<BonusStatisticVisitsResolver>();
                });

            //Only for tests
            CreateMap<BonusDto, AddBonusDto>();
            CreateMap<UserInfoDTO, ApplicationUser>();

            CreateMap<UpdateUserDTO, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, UserInfoDTO>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom<UserResolver>())
                .ReverseMap();

            CreateMap<ApplicationRole, RoleDTO>().ReverseMap();

            CreateMap<AddHistoryDTO, History>().AfterMap((src, dest) =>
            {
                dest.BonusId = src.BonusId;
                dest.CreatedDate = src.DateUse;
                dest.CreatorId = src.UserId;
                dest.Rating = src.Rating;

            }).ReverseMap();
            CreateMap<History, HistoryDto>().ReverseMap();
            CreateMap<History, BonusHistoryDto>().ReverseMap();
            CreateMap<History, UserHistoryDto>().ReverseMap();

            //From model to dto
            CreateMap<Vendor, VendorDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<AddVendorDto, Vendor>()
                .AfterMap((src, dest) => dest.Id = Guid.NewGuid());

        }
    }
}
