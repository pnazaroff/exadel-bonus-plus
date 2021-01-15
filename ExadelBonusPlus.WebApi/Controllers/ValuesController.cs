using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mongoSwager.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        List<Man> vendors = new List<Man>();
        public ValuesController()
        {
            vendors.Add(new Man(1, "Person1", "position1"));
            vendors.Add(new Man(2, "Person2", "position2"));
        }

        /// <summary>
        /// Get all vendors
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Man>> GetAllVendors()
        {
            
            vendors.Add(new Man(3, "Person3", "position3"));
            vendors.Add(new Man(4, "Person4", "position4"));
            if (vendors != null)
            {
                return vendors;
            }
            return NotFound();
        }
        /// <summary>
        /// Get  vendor by id
        /// </summary>
        /// <param name="id"></param> 
        [HttpGet("{id}")]
        public ActionResult<Man> GetAllVendors(int id)
        {
            var item = vendors.Find(c => c.id == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        /// <summary>
        /// Creates a Vendor.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "info": "info",
        ///        "Estimate" : 5,
        ///        "email" : "email"
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Man> CreateVendor(Man item)
        {
            int cnt = vendors.Count;
            vendors.Add(item);
            if (vendors.Count > cnt)
            {
                return item;
            }
            return NoContent();
        }
    }
}

public class Man
{
    public Man()
    {

    }
    public Man(int id, string name, string position)
    {
        this.id = id;
        this.name = name;
        this.position = position;
    }
    public int id { get; set; }
    public string name { get; set; }
    public string position { get; set; }
}
