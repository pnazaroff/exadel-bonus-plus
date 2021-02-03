using ExadelBonusPlus.Services.Interfaces;
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

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors(CancellationToken cancellationToken)
        {
            var vendors = await _vendorService.GetAllVendorsAsync(cancellationToken);
            return Ok(vendors);
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Vendor>> GetVendor(Guid id, CancellationToken cancellationToken)
        {
            var vendor = await _vendorService.GetVendorByIdAsync(id, cancellationToken);
            if (vendor == null)
            {
                return NotFound();
            }
            return Ok(vendor);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> SearchVendorByLocation(Location location, CancellationToken cancellationToken)
        {
            var vendor = await _vendorService.SearchVendorByLocation(location, cancellationToken);
            return Ok(vendor);
        }
        [HttpPost]
        public async Task AddVendor(Vendor model, CancellationToken cancellationToken)
        {
            await _vendorService.AddVendorAsync(model, cancellationToken);
        }

        [HttpPut]
        public async Task UpdateVendor(Vendor model, CancellationToken cancellationToken)
        {
            await _vendorService.UpdateVendorAsync(model, cancellationToken);
        }
        [HttpDelete]
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            await _vendorService.DeleteVendorAsync(id, cancellationToken);
        }
    }
}
