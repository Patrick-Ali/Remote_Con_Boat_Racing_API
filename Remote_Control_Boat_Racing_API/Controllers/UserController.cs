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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        //private static MongoClient mongoClient = new MongoClient();
        //private static MongoDB.Driver.IMongoDatabase db = mongoClient.GetDatabase("RCBR");
        //private static IMongoCollection<User> collection = db.GetCollection<User>("User");

        // GET: api/<controller>

        public UserController(UserService userService) {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {

            return _userService.Get();
            //var all = collection.Find(x => x.name == "John Smith").ToList();
            
            //var all = collection.Find(_ => true).ToList();
                        
            //return all; //new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);
            if (user == null) {
                return NotFound();
            }

            //var all = collection.Find(x => x.age == idd).ToList();
            return user;
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            User hold = _userService.Create(user);
            if (hold == null) {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status406NotAcceptable);
            }
            //Test test = new Test
            //{
            //    name = value.name,
            //    age = value.age
            //};

            //collection.InsertOneAsync(user);
            return CreatedAtRoute("GetUser", new { id = hold.Id.ToString() }, hold);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody] User userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Update(id, userIn);

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.Id);

            return NoContent();
        }

    }
}
