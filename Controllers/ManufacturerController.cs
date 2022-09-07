using API_Assignment.Data;
using API_Assignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Assignment.Controllers
{
    [Route("api/[controller]")] //VehicleManufacturer
    [ApiController]
    public class ManufacturerController : Controller
    {
        
        private readonly DatabaseContext _context;
        public ManufacturerController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Manufacturer> Get()
        {
            return _context.Manufacturers.ToArray();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public ActionResult<Manufacturer> Get(string id)
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
                Manufacturer found = _context.Manufacturers.Where(x => x.ID == providedID).Single();
                return found;
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult Post(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            try
            {
                _context.Manufacturers.Add(new Manufacturer() { Name = name});
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
            Manufacturer found;
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
                found = _context.Manufacturers.Where(x => x.ID == providedID).Single();
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

        //[HttpPatch("{id}")]
        //public ActionResult Patch(string id, string prop, string value)
        //{
        //    int providedID;
        //    Manufacturer found;
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
        //        found = _context.Manufacturers.Where(x => x.ID == providedID).Single();
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
        public ActionResult Delete(string id)
        {
            int providedID;
            Manufacturer found;
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
                found = _context.Manufacturers.Where(x => x.ID == providedID).Single();
            }
            catch
            {
                return NotFound();
            }
            try
            {
                _context.Manufacturers.Remove(found);
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
