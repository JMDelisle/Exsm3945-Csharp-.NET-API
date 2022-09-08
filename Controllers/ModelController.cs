using API_Assignment.Data;
using API_Assignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_Assignment.Controllers
{
    [Route("api/[controller]")]  //VehicleModels
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public ModelController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<VehicleModel> Get()
        {
            return _context.VehicleModels.ToArray();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public ActionResult<VehicleModel> Get(string id)
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
                VehicleModel found = _context.VehicleModels.Where(x => x.ID == providedID).Single();
                return found;
            }
            catch
            {
                return NotFound();
            }
        }

        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult Post(int manufacturerID, string name)
        {
            Manufacturer found;
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            try
            {
                found = _context.Manufacturers.Where(x => x.ID == manufacturerID).Single();
            }
            catch
            {
                return NotFound("It appears you have entered the wrong model informations, please reenter the correct informations! ");
            }

            try
            {
                _context.VehicleModels.Add(new VehicleModel() { Name = name, ManufacturerID = manufacturerID});
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(400);
            }
        }
        //public ActionResult Post(int manufacturerid, string name)
        //{
        //    Manufacturer test;
        //    if (string.IsNullOrWhiteSpace(name))
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        test = _context.Manufacturers.Where(x => x.ID == manufacturerid).Single();
        //    }
        //    catch
        //    {
        //        return NotFound("It appears you have entered the wrong name, please reenter a proper name.");
        //    }
        //    try
        //    {
        //        _context.VehicleModels.Add(new VehicleModel() { ManufacturerID = manufacturerid, Name = name });
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return StatusCode(400, "It appears you have entered the wrong informations! ");
        //    }
        //}

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, string name)
        {
            int providedID;
            VehicleModel found;
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
                found = _context.VehicleModels.Where(x => x.ID == providedID).Single();
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
                return StatusCode(404, "It appears something is missing from your data, please try again! ");
            }
        }

        //[HttpPatch("{id}")]
        //public ActionResult Patch(string id, string prop, string value)
        //{
        //    int providedID;
        //    VehicleModel found;
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
        //        found = _context.Models.Where(x => x.ID == providedID).Single();
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
        //        return StatusCode(404);
        //    }
        //}

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            int providedID;
            VehicleModel found;
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
                found = _context.VehicleModels.Where(x => x.ID == providedID).Single();
            }
            catch
            {
                return NotFound();
            }
            try
            {
                _context.VehicleModels.Remove(found);
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
