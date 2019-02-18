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
        //private static MongoClient mongoClient = new MongoClient();
        //private static MongoDB.Driver.IMongoDatabase db = mongoClient.GetDatabase("RCBR");
        //private static IMongoCollection<User> collection = db.GetCollection<User>("User");

        // GET: api/<controller>

        public BoatController(BoatService boatService)
        {
            _boatService = boatService;
        }

        [HttpGet]
        public ActionResult<List<Boat>> Get()
        {

            return _boatService.Get();
            //var all = collection.Find(x => x.name == "John Smith").ToList();

            //var all = collection.Find(_ => true).ToList();

            //return all; //new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetBoat")]
        public ActionResult<Boat> Get(string id)
        {
            var boat = _boatService.Get(id);
            if (boat == null)
            {
                return NotFound();
            }

            //var all = collection.Find(x => x.age == idd).ToList();
            return boat;
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Boat> Post([FromBody] Boat boat)
        {
            _boatService.Create(boat);
            //Test test = new Test
            //{
            //    name = value.name,
            //    age = value.age
            //};

            //collection.InsertOneAsync(user);
            return CreatedAtRoute("GetBoat", new { id = boat.Id.ToString() }, boat);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, Boat boatIn)
        {
            var boat = _boatService.Get(id);

            if (boat == null)
            {
                return NotFound();
            }

            _boatService.Update(id, boatIn);

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var boat = _boatService.Get(id);

            if (boat == null)
            {
                return NotFound();
            }

            _boatService.Remove(boat.Id);

            return NoContent();
        }
    }
}
