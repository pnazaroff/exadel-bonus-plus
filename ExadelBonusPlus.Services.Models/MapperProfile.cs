using System;
using System.Collections.Generic;
using System.Text;
using ExadelBonusPlus.Services.Models.DTO;
using AutoMapper;

namespace ExadelBonusPlus.Services.Models
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<PromotionDto, Promotion>().ReverseMap();
        }
    }
}
