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
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;
        //private static MongoClient mongoClient = new MongoClient();
        //private static MongoDB.Driver.IMongoDatabase db = mongoClient.GetDatabase("RCBR");
        //private static IMongoCollection<User> collection = db.GetCollection<User>("User");

        // GET: api/<controller>

        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public ActionResult<List<Team>> Get()
        {

            return _teamService.Get();
            //var all = collection.Find(x => x.name == "John Smith").ToList();

            //var all = collection.Find(_ => true).ToList();

            //return all; //new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetTeam")]
        public ActionResult<Team> Get(string id)
        {
            var teams = _teamService.Get(id);
            if (teams == null)
            {
                return NotFound();
            }

            //var all = collection.Find(x => x.age == idd).ToList();
            return teams;
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<Team> Post([FromBody] Team teams)
        {
            _teamService.Create(teams);
            //Test test = new Test
            //{
            //    name = value.name,
            //    age = value.age
            //};

            //collection.InsertOneAsync(user);
            return CreatedAtRoute("GetTeam", new { id = teams.Id.ToString() }, teams);
        }

        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, Team teamsIn)
        {
            var teams = _teamService.Get(id);

            if (teams == null)
            {
                return NotFound();
            }

            _teamService.Update(id, teamsIn);

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var teams = _teamService.Get(id);

            if (teams == null)
            {
                return NotFound();
            }

            _teamService.Remove(teams.Id);

            return NoContent();
        }
    }
}
