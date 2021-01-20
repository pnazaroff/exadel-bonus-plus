using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ExadelBonusPlus.DataAccess;
using ExadelBonusPlus.Services;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Annotations;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VendorsController : ControllerBase
    {
        private IGenericCrud _crudManager;

        public VendorsController(IGenericCrud crudManager)
        {
            _crudManager = crudManager;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            return await _crudManager.LoadRecords<Vendor>("Vendors");
        }

    }
}
