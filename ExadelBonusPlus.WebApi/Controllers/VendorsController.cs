using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ExadelBonusPlus.Services.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Annotations;

namespace ExadelBonusPlus.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendorsController : ControllerBase
    {
        private IMongoDatabase database;
        public VendorsController()
        {
            var client = new MongoClient("mongodb+srv://m001-student:m001-mongodb-basics@sandbox.yzrs7.mongodb.net/ExadelBonusPlus?retryWrites=true&w=majority");
            //db name is hard coded
            database = client.GetDatabase("ExadelBonusPlus");
        }
        [HttpGet("/api/Vendors")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            var collection = database.GetCollection<Vendor>("Vendors");

            return await collection.Find(new BsonDocument()).ToListAsync();
            
        }
    }
}
