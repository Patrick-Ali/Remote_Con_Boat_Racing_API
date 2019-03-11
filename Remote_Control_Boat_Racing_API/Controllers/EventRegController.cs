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
    public class EventRegController : ControllerBase
    {
        private readonly EventRegService _eventRegService;

        // GET: api/<controller>

        public EventRegController(EventRegService eventRegService)
        {
            _eventRegService = eventRegService;
        }

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

        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, EventReg eventsIn)
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
