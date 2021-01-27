using System;
using System.Collections.Generic;
using System.Text;
using ExadelBonusPlus.Services.Models.DTO;

namespace ExadelBonusPlus.Services.Models
{
    class MapperProfile: AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<Promotion, PromotionDTO>();
            CreateMap<PromotionDTO, Promotion>().ReverseMap();
        }
    }
}
