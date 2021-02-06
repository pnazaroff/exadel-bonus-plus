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
            CreateMap<Vendor, VendorDto>()
                .ForMember(vd=>vd.LocationDto, opts => opts.MapFrom(v=>v.Location));
            CreateMap<Location, LocationDto>();

        }
    }
}
