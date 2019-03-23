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
    /// for the event collection.
    /// </summary>
    [Route("api/1.0/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;


        // GET: api/<controller>
        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="eventService">
        /// Service class that contains the actions
        /// to implement required actions
        /// </param>
        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Get all events from the database
        /// </summary>
        /// <returns>
        /// If successful the events
        /// other wise returns a 500 internal
        /// server error http response.
        /// </returns>
        [HttpGet]
        public ActionResult<List<Event>> Get()
        {
            try
            {
                return _eventService.Get();
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get a specific event from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the event to get from the database
        /// </param>
        /// <returns>
        /// If successful the specific event
        /// other wise returns a 500 internal
        /// server error http response.
        /// </returns>
        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetEvent")]
        public ActionResult<EventIn> Get(string id)
        {
            try
            {
                var events = _eventService.Get(id);
                if (events == null)
                {
                    return NotFound();
                }

                return events;
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create a new event.
        /// </summary>
        /// <param name="events">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful the API address location of specific event
        /// other wise returns a 500 internal
        /// server error http response.
        /// </returns>
        // POST api/<controller>
        [HttpPost]
        public ActionResult<Event> Post([FromBody] EventIn events)
        {
            try
            {
                Event temp = _eventService.Create(events);

                return CreatedAtRoute("GetEvent", new { id = temp.Id.ToString() }, temp);
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update an event.
        /// </summary>
        /// <param name="id">
        /// ID of the event to be updated.
        /// </param>
        /// <param name="eventsIn">
        /// Updated information.
        /// </param>
        /// <returns>
        /// If successful 204 no content http response
        /// other wise returns a 500 internal
        /// server error http response.
        /// </returns>
        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody] Event eventsIn)
        {
            try
            {
                var events = _eventService.Get(id);

                if (events == null)
                {
                    return NotFound();
                }

                _eventService.Update(id, eventsIn);

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
        /// Delete an Event
        /// </summary>
        /// <param name="id">
        /// ID of the specific event
        /// </param>
        /// <returns>
        /// If successful the 204 no content http response
        /// other wise returns a 500 internal
        /// server error http response.
        /// </returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var events = _eventService.Get(id);

                if (events == null)
                {
                    return NotFound();
                }

                _eventService.Remove(events.Id);

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
