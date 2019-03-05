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
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;
        //private static MongoClient mongoClient = new MongoClient();
        //private static MongoDB.Driver.IMongoDatabase db = mongoClient.GetDatabase("RCBR");
        //private static IMongoCollection<User> collection = db.GetCollection<User>("User");

        // GET: api/<controller>

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public ActionResult<List<Event>> Get()
        {

            return _eventService.Get();
            //var all = collection.Find(x => x.name == "John Smith").ToList();

            //var all = collection.Find(_ => true).ToList();

            //return all; //new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetEvent")]
        public ActionResult<EventIn> Get(string id)
        {
            var events = _eventService.Get(id);
            if (events == null)
            {
                return NotFound();
            }

            //var all = collection.Find(x => x.age == idd).ToList();
            return events;
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Event> Post([FromBody] EventIn events)
        {
            Event temp = _eventService.Create(events);
            //Test test = new Test
            //{
            //    name = value.name,
            //    age = value.age
            //};

            //collection.InsertOneAsync(user);
            return CreatedAtRoute("GetEvent", new { id = temp.Id.ToString() }, temp);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, Event eventsIn)
        {
            var events = _eventService.Get(id);

            if (events == null)
            {
                return NotFound();
            }

            _eventService.Update(id, eventsIn);

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var events = _eventService.Get(id);

            if (events == null)
            {
                return NotFound();
            }

            _eventService.Remove(events.Id);

            return NoContent();
        }
    }
}
