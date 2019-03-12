using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Remote_Control_Boat_Racing_API.Models;
using Remote_Control_Boat_Racing_API.Services;

namespace Remote_Control_Boat_Racing_API.Controllers
{
    [Route("api/1.0/[controller]")]
    public class AdminLoginController : ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly AdminLoginService _adminLoginService;

        public AdminLoginController(AdminService adminService, AdminLoginService adminLoginService)
        {
            _adminService = adminService;
            _adminLoginService = adminLoginService;
        }

        public List<Admin> Get()
        {
            return _adminService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetAdmin2")]
        public ActionResult<Admin> Get(string id)
        {
            try
            {
                var admin = _adminService.Get(id);
                if (admin == null)
                {
                    return NotFound();
                }


                return admin;
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<Login> Login([FromBody] InLogin login)
        {
            try
            {
                List<Admin> admin = Get();

                Login log = _adminLoginService.Login(login.Email, login.Password, admin);

                if (log == null)
                {
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status406NotAcceptable);
                }

                return CreatedAtRoute("GetAdmin2", new { id = log.Id.ToString() }, log);
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{email}")]
        public ActionResult<bool> Post(string email)
        {
            try
            {
                List<Admin> users = Get();
                bool test = _adminLoginService.Check(email, users);
                //User hold = _userService.Create(user);
                if (test == false)
                {
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status406NotAcceptable);
                }

                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status202Accepted);
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