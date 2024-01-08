using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService service;
        public AuthController(IAuthService authService)
        {
            service = authService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]User user)
        {
            try
            {
                return Created("", service.RegisterUser(user));
            }
            catch (UserAlreadyExistsException uae)
            {
                return Conflict(uae.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong, please try later");
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]User user)
        {
            try
            {
                var isValid = service.LoginUser(user);
                if (isValid)
                {
                    return Ok(this.GetJWTToken(user.UserId));
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Unauthorized,"Invalid user id or password");
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Something went wrong, please try later {ex.Message}");
            }
        }

        private string GetJWTToken(string userId)
        {
            //setting the claims for the user credential name
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
            };

            //Defining security key and encoding the claim 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_auth_jwt_to_secure_microservice"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //defining the JWT token essential information and setting its expiration time
            var token = new JwtSecurityToken(
                issuer: "AuthenticationServer",
                audience: "AuthClient",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: creds
            );
            //defing the response of the token 
            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
            //convert into the json by serialing the response object
            return JsonConvert.SerializeObject(response);
        }
    }
}
