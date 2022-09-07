using API_Assignment.Data;
using API_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Assignment.Controllers
{
    [Route("api/[controller]")] //Dealership
    [ApiController]
    public class DealershipController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public DealershipController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Dealership> Get()
        {
            return _context.Dealerships.ToArray();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public ActionResult<Dealership> Get(string id)
        {
            int providedID;
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
                Dealership found = _context.Dealerships.Where(x => x.ID == providedID).Single();
                return found;
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult Post(string name,/* string manufacturerID, */string address, string phonenumber)
        {
            if (string.IsNullOrWhiteSpace(name) /*|| string.IsNullOrWhiteSpace(manufacturerID)*/ || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(phonenumber))
            {
                return BadRequest();
            }
            try
            {
                _context.Dealerships.Add(new Dealership() { Name = name, /*ManufacturerID = manufactID,*/ Address = address, PhoneNumber = phonenumber });
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(404);
            }
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string name, /*string manufactID,*/ string address, string phonenumber)
        {
            int providedID;
            Dealership found;
            try
            {
                providedID = int.Parse(name);
            }
            catch
            {
                return BadRequest();
            }
            try
            {
                found = _context.Dealerships.Where(x => x.ID == providedID).Single();
            }
            catch
            {
                return NotFound();
            }
            try
            {
                found.Name = name ?? found.Name;
                //found.ManufacturerID = manufactID ?? found.VehicleManufacturer;
                found.Address = address ?? found.Address;
                found.PhoneNumber = phonenumber ?? found.PhoneNumber;
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }

        //[HttpPatch("{id}")]
        //public ActionResult Patch(string ID, string prop, string value)
        //{
        //    int providedID;
        //    Dealership found;
        //    try
        //    {
        //        providedID = int.Parse(ID);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        found = _context.Dealerships.Where(x => x.ID == providedID).Single();
        //    }
        //    catch
        //    {
        //        return NotFound();
        //    }
        //    try
        //    {
        //        switch (prop)
        //        {
        //            case "name":
        //                found.Name = value;
        //                break;
        //            //case "manuID":
        //            //    found.ManufacturerID = value;
        //            //    break;
        //            case "address":
        //                found.Address = value;
        //                break;
        //            case "phonenumber":
        //                found.PhoneNumber = value;
        //                break;
        //            default:
        //                return BadRequest();
        //        }
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }
        //}

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string ID)
        {
            int providedID;
            Dealership found;
            try
            {
                providedID = int.Parse(ID);
            }
            catch
            {
                return BadRequest();
            }
            try
            {
                found = _context.Dealerships.Where(x => x.ID == providedID).Single();
            }
            catch
            {
                return NotFound();
            }
            try
            {
                _context.Dealerships.Remove(found);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(404); // 400 & 404 Error.
            }
        }
    }
}
