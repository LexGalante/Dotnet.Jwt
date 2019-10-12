using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Dotnet.Jwt.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Settings _settings;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IOptions<Settings> options)
        {
            _logger = logger;
            _settings = options.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Token([FromBody] string login, [FromBody] string password)
        {
            var user = Usuario.GetUserFake(login, password);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Role, user.Roles.First().Name));

            var handler = new JwtSecurityTokenHandler();

            var expiresAt = DateTime.Now.AddMinutes(10);

            var token = handler.WriteToken(handler.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = claims,
                Issuer = _settings.ValidIssuers[0],
                Audience = _settings.ValidIssuers[0],
                Expires = expiresAt,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)),
                        SecurityAlgorithms.Aes128CbcHmacSha256)
            }));

            return Ok(new 
            {
                AcessToken = token,
                ExpiresAt = expiresAt
            });
        }

        //Ao consumir este endpoint voce precisa enviar o header Authentication: Bearer <jwt_token>
        [HttpGet]
        [Authorize(Roles = "admin,xpto")]
        public IActionResult Teste()
        {
            return Ok();
        }
    }
}
