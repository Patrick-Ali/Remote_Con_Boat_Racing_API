using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Remote_Control_Boat_Racing_API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Remote_Control_Boat_Racing_API.Controllers
{
    [Route("api/1.0/[controller]")]
    public class BoatController : ControllerBase
    {
        private readonly BoatService  _boatService;


        // GET: api/<controller>

        public BoatController(BoatService boatService)
        {
            _boatService = boatService;
        }

        [HttpGet]
        public ActionResult<List<Boat>> Get()
        {
            try
            {
                return _boatService.Get();
            }
            catch(Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetBoat")]
        public ActionResult<Boat> Get(string id)
        {
            try
            {
                var boat = _boatService.Get(id);
                if (boat == null)
                {
                    return NotFound();
                }

                return boat;
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Boat> Post([FromBody] Boat boat)
        {
            try
            {
               Boat hold = _boatService.Create(boat);
                if (hold == null)
                {
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status406NotAcceptable);
                }

                return CreatedAtRoute("GetBoat", new { id = boat.Id.ToString() }, boat);
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody] Boat boatIn)
        {
            try
            {
                var boat = _boatService.Get(id);

                if (boat == null)
                {
                    return NotFound();
                }

                _boatService.Update(id, boatIn);

                return NoContent();
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var boat = _boatService.Get(id);

                if (boat == null)
                {
                    return NotFound();
                }

                _boatService.Remove(boat.Id);

                return NoContent();
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }
    }
}
