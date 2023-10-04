using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestePonta.Core.DTOs;
using TestePonta.Domain.Services;

namespace TestePonta.Controllers
{
    /// <summary>
    /// Controlador de autenticação.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        /// <summary>
        /// Construtor do controlador de autenticação.
        /// </summary>
        /// <param name="configuration">A configuração da aplicação.</param>
        /// <param name="userService">O serviço de usuário.</param>
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        /// <summary>
        /// Efetua o login de um usuário.
        /// </summary>
        /// <param name="model">As informações de login do usuário.</param>
        /// <returns>Um token JWT se o login for bem-sucedido.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            _logger.Trace("Pedido de login: {0}|{1}", model.Email, model.Password);
            var user = await _userService.AuthorizeUser(model.Email, model.Password);
            if (user == null)
            {
                _logger.Warn("Pedido de login negado: {0}|{1}", model.Email, model.Password);
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
