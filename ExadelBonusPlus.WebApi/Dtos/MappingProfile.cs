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
                .ForMember(vd=>vd.Location, opts => opts.MapFrom(v=>v.Location));
            CreateMap<Location, LocationDto>();

            //From dto to model 
            CreateMap<VendorDto, Vendor>()
                .ForMember(v => v.Location, opts => opts.MapFrom(vd => new Location { City = vd.Location.City, Address =vd.Location.Address, Country = vd.Location.Country, Lattitude=vd.Location.Lattitude, Longtitude=vd.Location.Longtitude }));

        }
    }
}
