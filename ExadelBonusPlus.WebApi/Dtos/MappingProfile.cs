using AutoMapper;
using ExadelBonusPlus.Services.Models;
using System.Linq;

namespace ExadelBonusPlus.WebApi.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //From model to dto
            CreateMap<Vendor, VendorDto>();
            CreateMap<Location, LocationDto>();

            //from dto to model
            CreateMap<VendorDto, Vendor>();
            CreateMap<LocationDto, Location>();
        }
    }
}
