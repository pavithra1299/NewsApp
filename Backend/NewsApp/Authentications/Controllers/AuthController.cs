using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Authentications.Models;
using Authentications.Exceptions;
using Authentications.Services;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using Azure.Storage.Queues;
using System.Net.Http;

namespace Authentications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;
        private readonly ILogger _logger;
        private static readonly HttpClient _client = new HttpClient();
        private readonly string functionUrl = "https://functionapp120231228081318.azurewebsites.net/api/Function1?code=sEaO27i7V6KYDKbfeekpKCngCGAVeU9V8shEPvN3Epj_AzFuQHprsQ==";
        public AuthController( IAuthService authService, ILogger<AuthController> logger)
        {
            service = authService;
            _logger = logger;
            //_httpClientFactory = httpClientFactory;
            _logger.LogInformation(message: "Constructor for user authentication called");
        }

       

        // POST api/<controller>
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Login user)
        {
            try
            {
                var isValid = service.LoginUser(user);
                if (isValid)
                {
                    _logger.LogInformation(message: $"User {user.UserId} logged in successfully and token generated");
                    
                    string successMessage = $"Login successful for user: {user.UserId}"; 
                    var content = new StringContent(successMessage, Encoding.UTF8, "application/json");
                    var response = _client.PostAsync(functionUrl, content);
                    return Ok(this.GetJWTToken(user.UserId));
                   
                }
                else
                {
                    _logger.LogInformation(message: "Invalid user");
                    return StatusCode((int)HttpStatusCode.Unauthorized, "Invalid user id or password");
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
