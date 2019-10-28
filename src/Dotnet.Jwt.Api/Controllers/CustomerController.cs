using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dotnet.Jwt.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Dotnet.Jwt.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly Settings _settings;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger,
            IOptions<Settings> options,
            ICustomerService customerService)
        {
            _logger = logger;
            _settings = options.Value;
            _customerService = customerService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Token([FromBody] string email, [FromBody] string password)
        {
            var customer = _customerService.Get()
                                        .SingleOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Role, customer.Roles.First().Name));

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

    }
}
