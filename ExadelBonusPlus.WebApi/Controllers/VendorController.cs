using ExadelBonusPlus.Services.Interfaces;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            return await _vendorService.GetAllVendorsAsync();
        }
    }
}
