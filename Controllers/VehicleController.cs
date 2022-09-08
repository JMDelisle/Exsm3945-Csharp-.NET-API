using API_Assignment.Data;
using API_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

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
        public ActionResult<Vehicle> Get(string vin)
        {
            string providedID;
            try
            {
                providedID = vin;
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
        public ActionResult Post(string vin, int modelid, int dealershipid, string trim)
        {
            Dealership test2;
            Manufacturer test;
            //if (string.IsNullOrEmpty(vin))
            //{
            //    throw new ArgumentException($"'{nameof(vin)}' cannot be null or empty.", nameof(vin));
            //}

            //if (string.IsNullOrEmpty(trim))
            //{
            //    throw new ArgumentException($"'{nameof(trim)}' cannot be null or empty.", nameof(trim));
            //}

            if (string.IsNullOrWhiteSpace(vin) || string.IsNullOrWhiteSpace(trim))
            {
                return BadRequest();
            }

            try
            {
                test = _context.Manufacturers.Where(x => x.ID == modelid).Single();
            }
            catch
            {
                return NotFound("It appears you have entered the wrong informations in modelid, please reenter the correct informations! ");
            }

            try
            {
                test2 = _context.Dealerships.Where(x => x.ID == dealershipid).Single();
            }
            catch
            {
                return NotFound("It appears you have entered the wrong informations in dealershipid, please reenter the correct informations! ");
            }

            try
            {
                _context.Vehicles.Add(new Vehicle() { VIN = vin, ModelID = modelid, DealershipID = dealershipid, TrimLevel = trim });
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(400, "It appears you have entered the wrong informations! ");
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string vin, string trimlevel)
        {
            string providedID;
            Vehicle found;
            try
            {
                providedID = (vin);
            }
            catch
            {
                return BadRequest();
            }
            try
            {
                found = _context.Vehicles.Where(x => x.VIN == providedID).Single();
            }
            catch
            {
                return NotFound();
            }
            try
            {
                found.VIN = vin ?? found.VIN;
                found.TrimLevel = trimlevel ?? found.TrimLevel;
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(404, "It appears something is missing from your data, please try again! ");
            }
        }

        //[HttpPatch("{id}")]
        //public ActionResult Patch(string id, string prop, string value)
        //{
        //    int providedID;
        //    Vehicle found;
        //    try
        //    {
        //        providedID = int.Parse(id);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        found = _context.Vehicles.Where(x => x.ID == providedID).Single();
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //    try
        //    {
        //        switch (prop)
        //        {
        //            case "Model name": // Displayed for viewers.
        //                found.Name = value;
        //                break;
        //            default:
        //                return BadRequest();
        //        }
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return StatusCode(400);
        //    }
        //}

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string vin)
        {
            string providedID;
            Vehicle found;
            try
            {
                providedID = (vin);
            }
            catch
            {
                return BadRequest();
            }
            try
            {
                found = _context.Vehicles.Where(x => x.VIN == providedID).Single();
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
                return StatusCode(400, "Cannot find the id, Are you sure your using the correct ones? ");
            }
        }
    }
}
