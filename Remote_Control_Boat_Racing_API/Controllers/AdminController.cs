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
    /// for the admin collection.
    /// </summary>
    [Route("api/1.0/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="adminService">
        /// Service class that contains the actions
        /// to implement required actions
        /// </param>
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Get all admins from the database
        /// </summary>
        /// <returns>
        /// If successful returns all the admins
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Admin>> Get()
        {
            try
            {
                return _adminService.Get();
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Get a specific admin from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the admin to get from the database
        /// </param>
        /// <returns>
        /// If successful returns the specific admin
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        // GET api/<controller>/5
        [HttpGet("{id:length(24)}", Name = "GetAdmin")]
        public ActionResult<Admin> Get(string id)
        {
            try
            {
                var user = _adminService.Get(id);
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

        /// <summary>
        /// Create a new admin.
        /// </summary>
        /// <param name="admin">
        /// Information to be added to the
        /// database.
        /// </param>
        /// <returns>
        /// If successful returns the API address location of specific admin
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        // POST api/<controller>
        [HttpPost]
        public ActionResult<Admin> Post([FromBody] Admin admin)
        {
            try
            {
                Admin hold = _adminService.Create(admin);
                if (hold == null)
                {
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status406NotAcceptable);
                }

                return CreatedAtRoute("GetAdmin", new { id = hold.Id.ToString() }, hold);
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Update an admin.
        /// </summary>
        /// <param name="id">
        /// ID of the admin to be updated.
        /// </param>
        /// <param name="adminIn">
        /// Updated information.
        /// </param>
        /// <returns>
        /// If successful returns 204 no content http response
        /// other wise returns a 500 internal
        /// server error http response.
        /// </returns>
        // PUT api/<controller>/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody] Admin adminIn)
        {
            try
            {
                var user = _adminService.Get(id);

                if (user == null)
                {
                    return NotFound();
                }

                _adminService.Update(id, adminIn);

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
        /// Delete an Admin
        /// </summary>
        /// <param name="id">
        /// ID of the specific admin
        /// </param>
        /// <returns>
        /// If successful returns the 204 no content http response
        /// other wise returns a 500 internal
        /// server error http response.
        /// </returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var admin = _adminService.Get(id);

                if (admin == null)
                {
                    return NotFound();
                }

                _adminService.Remove(admin.Id);

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
