﻿using System;
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
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly LoginService _loginService;

        public LoginController(UserService userService, LoginService loginService)
        {
            _userService = userService;
            _loginService = loginService;
        }

        public List<User> Get()
        {
            return _userService.Get();
        }

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