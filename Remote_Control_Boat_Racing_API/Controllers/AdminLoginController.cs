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
    /// <summary>
    /// This controller is responsible for managing requests
    /// for Admin Login services.
    /// </summary>
    [Route("api/1.0/[controller]")]
    public class AdminLoginController : ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly AdminLoginService _adminLoginService;

        // GET: api/<controller>
        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="adminService">
        /// Service class that contains the actions
        /// to get information from the admin collection.
        /// </param>
        /// <param name="adminLoginService">
        /// Service class that contains the actions
        /// needed to perfrom the Admin login actions. 
        /// </param>
        public AdminLoginController(AdminService adminService, AdminLoginService adminLoginService)
        {
            _adminService = adminService;
            _adminLoginService = adminLoginService;
        }

        /// <summary>
        /// Get all admins from the database
        /// </summary>
        /// <returns>
        /// If successful rewturn all the admins
        /// other wise returns null.
        /// </returns>
        public List<Admin> Get()
        {
            try
            { 
                return _adminService.Get();
            }
            catch (Exception e)
            {
                string message = e.Message;
                string stackTrace = e.StackTrace;

                return null;
            }
        }

        /// <summary>
        /// Get a specific admin from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the admin to get from the database
        /// </param>
        /// <returns>
        /// If successful the specific admin
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
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

        /// <summary>
        /// This action will handel admin login requests.
        /// </summary>
        /// <param name="login">
        /// Admin login information sent for processing.
        /// </param>
        /// <returns>
        /// If login successful it will return the location of
        /// the admin in the API.
        /// Otherwise it will return a 406 responce of unacceptable.
        /// Worst case it returns 500 internal server error.
        /// </returns>
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

        /// <summary>
        /// This action can be used to determin if the given 
        /// email is associated with any admin.
        /// </summary>
        /// <param name="email">
        /// The email address submited for consideration.
        /// </param>
        /// <returns>
        /// If the email is found it will return a 202 response code
        /// of acceptable to signal the email is associated with an
        /// admin.
        /// Otherwise it will return a 406 responce code of unacceptable.
        /// Worst case it returns 500 internal server error.
        /// </returns>
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