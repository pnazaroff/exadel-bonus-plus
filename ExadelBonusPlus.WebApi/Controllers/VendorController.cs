using AutoMapper;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VendorController : ControllerBase
    {
        private IVendorService _vendorService;
        private readonly IMapper _mapper;

        public VendorController(IVendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorDto>>> GetVendors(CancellationToken cancellationToken)
        {
            var vendors = await _vendorService.GetAllVendorsAsync(cancellationToken);
            var vendorsDto = _mapper.Map<IEnumerable<Vendor>, IEnumerable<VendorDto>>(vendors);
            return Ok(vendorsDto);
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<VendorDto>> GetVendor(Guid id, CancellationToken cancellationToken)
        {
            var vendor = await _vendorService.GetVendorByIdAsync(id, cancellationToken);

            if (vendor == null)
            {
                return NotFound();
            }
            var vendorDto = _mapper.Map<Vendor, VendorDto>(vendor);
            
            return Ok(vendorDto);
        }
        [HttpPost]
        public async Task AddVendor([FromBody]VendorDto model, CancellationToken cancellationToken)
        {
            var vendor = _mapper.Map<VendorDto, Vendor>(model);
            vendor.CreatedDate = DateTime.Now;
            vendor.IsDeleted = false;

            await _vendorService.AddVendorAsync(vendor, cancellationToken);
        }

        [HttpPut]
        public async Task UpdateVendor(VendorDto model, CancellationToken cancellationToken)
        {
            var vendor = _mapper.Map<VendorDto, Vendor>(model);
            vendor.ModifiedDate = DateTime.Now;

            await _vendorService.UpdateVendorAsync(vendor, cancellationToken);
        }
        [HttpDelete]
        public async Task DeleteVendor(Guid id, CancellationToken cancellationToken)
        {
            //should be working on deletion 
            await _vendorService.DeleteVendorAsync(id, cancellationToken);
        }

        [HttpGet("{city}")]
        public async Task<ActionResult<VendorDto>> GetVendorByCity(string city, CancellationToken cancellationToken)
        {
            var vendor = await _vendorService.SearchVendorByLocationAsync(city, cancellationToken);

            if (vendor == null)
            {
                return NotFound();
            }
            var vendorDto = _mapper.Map<IEnumerable<Vendor>, IEnumerable<VendorDto>>(vendor);

            return Ok(vendorDto);
        }



    }
}
