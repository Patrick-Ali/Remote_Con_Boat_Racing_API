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
        //private static MongoClient mongoClient = new MongoClient();
        //private static MongoDB.Driver.IMongoDatabase db = mongoClient.GetDatabase("RCBR");
        //private static IMongoCollection<User> collection = db.GetCollection<User>("User");

        // GET: api/<controller>

        public EventRegController(EventRegService eventRegService)
        {
            _eventRegService = eventRegService;
        }

        [HttpGet]
        public ActionResult<List<EventReg>> Get()
        {

            return _eventRegService.Get();
            //var all = collection.Find(x => x.name == "John Smith").ToList();

            //var all = collection.Find(_ => true).ToList();

            //return all; //new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetEventReg")]
        public ActionResult<EventReg> Get(string id)
        {
            var eventsReg = _eventRegService.Get(id);
            if (eventsReg == null)
            {
                return NotFound();
            }

            //var all = collection.Find(x => x.age == idd).ToList();
            return eventsReg;
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<EventReg> Post([FromBody] EventReg eventsReg)
        {
            _eventRegService.Create(eventsReg);
            //Test test = new Test
            //{
            //    name = value.name,
            //    age = value.age
            //};

            //collection.InsertOneAsync(user);
            return CreatedAtRoute("GetEventReg", new { id = eventsReg.Id.ToString() }, eventsReg);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, EventReg eventsIn)
        {
            var events = _eventRegService.Get(id);

            if (events == null)
            {
                return NotFound();
            }

            _eventRegService.Update(id, eventsIn);

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var events = _eventRegService.Get(id);

            if (events == null)
            {
                return NotFound();
            }

            _eventRegService.Remove(events.Id);

            return NoContent();
        }
    }
}
