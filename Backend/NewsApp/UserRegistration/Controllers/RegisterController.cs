using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;
using UserRegistration.Exceptions;
using UserRegistration.Services;

namespace UserRegistration.Controllers
{
    [ApiController]
    [Route("api/user")]

    public class RegisterController : ControllerBase
    {

        private readonly IRegisterService _registerService;
        private readonly ILogger _logger;

        public RegisterController(IRegisterService sevice, ILogger<RegisterController> logger)
        {
            _registerService = sevice;
            _logger = logger;
            _logger.LogInformation(message: "Constructor for user registration called");
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                bool createdUser = _registerService.RegisterUser(user);
                _logger.LogInformation(message: $"User {user.UserName} created Successfully!!");
                return StatusCode(201, createdUser);
            }
            catch (UserNotCreatedException ex)
            {
                return Conflict(ex.Message);
                _logger.LogInformation(message: $"User with username {user.UserName} already exists");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: "Internal server error");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult GetUserByName(string name)
        {
            try
            {
                var user = _registerService.GetByName(name);
                _logger.LogInformation(message: $"User with username {user.UserName} displayed");
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogInformation(message: "User with username not found");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(message: "Internal Server error");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }




    }
}
