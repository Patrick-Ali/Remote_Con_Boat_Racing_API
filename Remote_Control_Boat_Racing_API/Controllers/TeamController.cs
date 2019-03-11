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

        
        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Team>> Get()
        {
            try
            {
                return _teamService.Get();
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }

        }

        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetTeam")]
        public ActionResult<Team> Get(string id)
        {
            try
            {
                var teams = _teamService.Get(id);
                if (teams == null)
                {
                    return NotFound();
                }

                return teams;
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
        public ActionResult<Team> Post([FromBody] Team teams)
        {
            try
            {
                _teamService.Create(teams);

                return CreatedAtRoute("GetTeam", new { id = teams.Id.ToString() }, teams);
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
        public IActionResult Put(string id, Team teamsIn)
        {
            try
            {
                var teams = _teamService.Get(id);

                if (teams == null)
                {
                    return NotFound();
                }

                _teamService.Update(id, teamsIn);

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
                var teams = _teamService.Get(id);

                if (teams == null)
                {
                    return NotFound();
                }

                _teamService.Remove(teams.Id);

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
