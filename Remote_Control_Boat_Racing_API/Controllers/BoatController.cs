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
    /// <summary>
    /// This controller is responsible for managing requests
    /// for the boat collection.
    /// </summary>
    [Route("api/1.0/[controller]")]
    public class BoatController : ControllerBase
    {
        private readonly BoatService  _boatService;


        // GET: api/<controller>
        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="boatService">
        /// Service class that contains the actions
        /// to implement required actions
        /// </param>
        public BoatController(BoatService boatService)
        {
            _boatService = boatService;
        }

        /// <summary>
        /// Get all boats from the database
        /// </summary>
        /// <returns>
        /// If successful return all the boats
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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

        /// <summary>
        /// Get a specific boat from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the boat to get from the database.
        /// </param>
        /// <returns>
        /// If successful the specific boat
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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

        /// <summary>
        /// Create a new boat.
        /// </summary>
        /// <param name="boat">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful the API address location of specific boat
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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

        /// <summary>
        /// Update a boat.
        /// </summary>
        /// <param name="id">
        /// ID of the boat to be updated.
        /// </param>
        /// <param name="boatIn">
        /// Updated information.
        /// </param>
        /// <returns>
        /// If successful 204 no content http response
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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

        /// <summary>
        /// Delete a Boat
        /// </summary>
        /// <param name="id">
        /// ID of the specific boat
        /// </param>
        /// <returns>
        /// If successful the 204 no content http response
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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
