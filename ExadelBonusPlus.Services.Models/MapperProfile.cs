using AutoMapper;
using ExadelBonusPlus.Services.Models.ViewModel;

namespace ExadelBonusPlus.Services.Models
{
    class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<UserInfo, ApplicationUser>().ReverseMap();
          
        }
    }
}
