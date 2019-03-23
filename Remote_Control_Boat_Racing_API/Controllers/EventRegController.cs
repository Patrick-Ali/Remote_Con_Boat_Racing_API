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
    /// for the event reg collection.
    /// </summary>
    [Route("api/1.0/[controller]")]
    public class EventRegController : ControllerBase
    {
        private readonly EventRegService _eventRegService;

        // GET: api/<controller>

        // GET: api/<controller>
        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="eventRegService">
        /// Service class that contains the actions
        /// to implement required actions
        /// </param>
        public EventRegController(EventRegService eventRegService)
        {
            _eventRegService = eventRegService;
        }

        /// <summary>
        /// Get all event regs from the database
        /// </summary>
        /// <returns>
        /// If successful return all the event regs
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        [HttpGet]
        public ActionResult<List<EventReg>> Get()
        {
            try
            {
                return _eventRegService.Get();
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get a specific event reg from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the event reg to get from the database
        /// </param>
        /// <returns>
        /// If successful the specific event reg
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetEventReg")]
        public ActionResult<EventReg> Get(string id)
        {
            try
            {
                var eventsReg = _eventRegService.Get(id);
                if (eventsReg == null)
                {
                    return NotFound();
                }

                return eventsReg;
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create a new event reg.
        /// </summary>
        /// <param name="eventsReg">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful the API address location of specific event reg
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        // POST api/<controller>
        [HttpPost]
        public ActionResult<EventReg> Post([FromBody] EventReg eventsReg)
        {
            try
            {
                _eventRegService.Create(eventsReg);

                return CreatedAtRoute("GetEventReg", new { id = eventsReg.Id.ToString() }, eventsReg);
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update an event reg.
        /// </summary>
        /// <param name="id">
        /// ID of the event reg to be updated.
        /// </param>
        /// <param name="eventsIn">
        /// Updated information.
        /// </param>
        /// <returns>
        /// If successful 204 no content http response
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody] EventReg eventsIn)
        {
            try
            {
                var events = _eventRegService.Get(id);

                if (events == null)
                {
                    return NotFound();
                }

                _eventRegService.Update(id, eventsIn);

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
        /// Delete an EventReg
        /// </summary>
        /// <param name="id">
        /// ID of the specific event reg
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
                var events = _eventRegService.Get(id);

                if (events == null)
                {
                    return NotFound();
                }

                _eventRegService.Remove(events.Id);

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
