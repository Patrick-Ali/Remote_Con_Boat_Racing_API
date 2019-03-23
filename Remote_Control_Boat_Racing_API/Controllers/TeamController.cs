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
    /// for the team collection.
    /// </summary>
    [Route("api/1.0/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;

        // GET: api/<controller>
        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="teamService">
        /// Service class that contains the actions
        /// to implement required actions
        /// </param>
        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        // GET: api/<controller>
        /// <summary>
        /// Get all teams from the database
        /// </summary>
        /// <returns>
        /// If successful return all the teams
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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
        /// <summary>
        /// Get a specific team from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the team to get from the database.
        /// </param>
        /// <returns>
        /// If successful the specific team
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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
        /// <summary>
        /// Create a new team.
        /// </summary>
        /// <param name="teams">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful the API address location of specific team
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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
        /// <summary>
        /// Update a team.
        /// </summary>
        /// <param name="id">
        /// ID of the team to be updated.
        /// </param>
        /// <param name="teamsIn">
        /// Updated information.
        /// </param>
        /// <returns>
        /// If successful 204 no content http response
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody] Team teamsIn)
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
        /// <summary>
        /// Delete a team
        /// </summary>
        /// <param name="id">
        /// ID of the specific team
        /// </param>
        /// <returns>
        /// If successful the 204 no content http response
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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
