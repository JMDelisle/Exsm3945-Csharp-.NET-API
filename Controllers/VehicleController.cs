using API_Assignment.Data;
using API_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Assignment.Controllers
{
    [Route("api/[controller]")] //VehicleModel
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public VehicleController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Vehicle> Get()
        {
            return _context.Vehicles.ToArray();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public ActionResult<Vehicle> Get(string VIN)
        {
            int providedID;
            try
            {
                providedID = int.Parse(VIN);
            }
            catch
            {
                return BadRequest();
            }
            try
            {
                Vehicle found = _context.Vehicles.Where(x => x.VIN == providedID).Single();
                return found;
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult Post(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin))
            {
                return BadRequest();
            }
            try
            {
                _context.Models.Add(new Vehicle() { VIN = vin});
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, string name)
        {
            int providedID;
            Vehicle found;
            try
            {
                providedID = int.Parse(id);
            }
            catch
            {
                return BadRequest();
            }
            try
            {
                found = _context.Vehicles.Where(x => x.ID == providedID).Single();
            }
            catch
            {
                return NotFound();
            }
            try
            {
                found.Name = name ?? found.Name;
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(404);
            }
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(string id, string prop, string value)
        {
            int providedID;
            Vehicle found;
            try
            {
                providedID = int.Parse(id);
            }
            catch
            {
                return BadRequest();
            }
            try
            {
                found = _context.Vehicles.Where(x => x.ID == providedID).Single();
            }
            catch
            {
                return NotFound();
            }
            try
            {
                switch (prop)
                {
                    case "Model name": // Displayed for viewers.
                        found.Name = value;
                        break;
                    default:
                        return BadRequest();
                }
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            int providedID;
            Vehicle found;
            try
            {
                providedID = int.Parse(id);
            }
            catch
            {
                return BadRequest();
            }
            try
            {
                found = _context.Vehicles.Where(x => x.ID == providedID).Single();
            }
            catch
            {
                return NotFound();
            }
            try
            {
                _context.Vehicles.Remove(found);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(404);
            }
        }
    }
}
