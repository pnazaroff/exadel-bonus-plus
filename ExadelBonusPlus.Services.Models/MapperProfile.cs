using AutoMapper;
using ExadelBonusPlus.WebApi.Controllers;

namespace ExadelBonusPlus.Services.Models
{
    class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<HistoryDto, History>().ReverseMap();
            CreateMap<UserInfo, ApplicationUser>().ReverseMap();
        }
    }
}
