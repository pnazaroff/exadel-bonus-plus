using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ExadelBonusPlus.Services.Models
{
    public class BonusResolver : IValueResolver<Bonus, BonusDto, VendorDto>
    {
        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;
        public BonusResolver(IVendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }

        public  VendorDto Resolve(Bonus source, BonusDto destination, VendorDto destMember, ResolutionContext context)
        {
                try
                {
                    return _mapper.Map<VendorDto>(_vendorService.GetVendorByIdAsync(source.CompanyId).GetAwaiter()
                        .GetResult());
                }
                catch
                {
                    //Vendor does not finded by Id
                    return new VendorDto();
                }
        }
    }
}
