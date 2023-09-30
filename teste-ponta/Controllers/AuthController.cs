using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TestePonta.Core.DTOs;
using TestePonta.Domain.Services;

namespace TestePonta.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var user = await _userService.AuthorizeUser(model.Email, model.Password);
            if(user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(model.Email, user.Id);

            return Ok(new { token });
        }

        private string GenerateJwtToken(string email, int id)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
               _configuration["Jwt:Audience"],
               claims,
               expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
               signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
