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
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;



        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

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
