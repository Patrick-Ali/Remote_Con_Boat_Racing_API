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
    /// for User Login services.
    /// </summary>
    [Route("api/1.0/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly LoginService _loginService;

        /// <summary>
        /// Initalisation action
        /// </summary>
        /// <param name="userService">
        /// Service class that contains the actions
        /// to get information from the user collection.
        /// </param>
        /// <param name="loginService">
        /// Service class that contains the actions
        /// needed to perfrom the user login actions. 
        /// </param>
        public LoginController(UserService userService, LoginService loginService)
        {
            _userService = userService;
            _loginService = loginService;
        }

        /// <summary>
        /// Get all users from the database
        /// </summary>
        /// <returns>
        /// If successful rewturn all the users
        /// </returns>
        public List<User> Get()
        {
            return _userService.Get();
        }

        /// <summary>
        /// Get a specific user from the database. 
        /// </summary>
        /// <param name="id">
        /// ID of the user to get from the database
        /// </param>
        /// <returns>
        /// If successful the specific user
        /// otherwise returns a 500 internal
        /// server error http response.
        /// </returns>
        [HttpGet("{id:length(24)}", Name = "GetUser2")]
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

        /// <summary>
        /// This action will handel user login requests.
        /// </summary>
        /// <param name="login">
        /// Admin login information sent for processing.
        /// </param>
        /// <returns>
        /// If login successful it will return the location of
        /// the user in the API.
        /// Otherwise it will return a 406 responce of unacceptable.
        /// Worst case it returns 500 internal server error.
        /// </returns>
        [HttpPost]
        public ActionResult<Login> Login([FromBody] InLogin login)
        {
            try
            {
                List<User> users = Get();

                Login log = _loginService.Login(login.Email, login.Password, users);

                if (log == null)
                {
                    return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status406NotAcceptable);
                }
                
                return CreatedAtRoute("GetUser2", new { id = log.Id.ToString() }, log);
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
        /// email is associated with any user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// If the email is found it will return a 202 response code
        /// of acceptable to signal the email is associated with a
        /// user.
        /// Otherwise it will return a 406 responce code of unacceptable.
        /// Worst case it returns 500 internal server error.
        /// </returns>
        [HttpPost("{email}")]
        public ActionResult<bool> Post(string email)
        {
            try
            {
                List<User> users = Get();
                bool test = _loginService.Check(email, users);
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