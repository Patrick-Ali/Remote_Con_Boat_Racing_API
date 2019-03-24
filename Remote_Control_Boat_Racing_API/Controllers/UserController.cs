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
    /// for the user collection.
    /// </summary>
    [Route("api/1.0/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;


        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="userService">
        /// Service class that contains the actions
        /// to implement required actions
        /// </param>
        public UserController(UserService userService) {
            _userService = userService;
        }

        // GET: api/<controller>
        /// <summary>
        /// Get all users from the database
        /// </summary>
        /// <returns>
        /// If successful returns all the users
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            try
            {
                return _userService.Get();
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
        /// Get a specific user from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the user to get from the database.
        /// </param>
        /// <returns>
        /// If successful returns the specific user
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            try
            {
                var user = _userService.Get(id);
                if (user == null)
                {
                    return NotFound();
                }

                return user;
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
        /// Create a new user.
        /// </summary>
        /// <param name="user">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful returns the API address location of specific user
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                User hold = _userService.Create(user);
                if (hold == null)
                {
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status406NotAcceptable);
                }

                return CreatedAtRoute("GetUser", new { id = hold.Id.ToString() }, hold);
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
        /// Update a user.
        /// </summary>
        /// <param name="id">
        /// ID of the user to be updated.
        /// </param>
        /// <param name="userIn">
        /// Updated information.
        /// </param>
        /// <returns>
        /// If successful returns 204 no content http response
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody] User userIn)
        {
            try
            {
                var user = _userService.Get(id);

                if (user == null)
                {
                    return NotFound();
                }

                _userService.Update(id, userIn);

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
        /// Delete a user
        /// </summary>
        /// <param name="id">
        /// ID of the specific user
        /// </param>
        /// <returns>
        /// If successful returns the 204 no content http response
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var user = _userService.Get(id);

                if (user == null)
                {
                    return NotFound();
                }

                _userService.Remove(user.Id);

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
