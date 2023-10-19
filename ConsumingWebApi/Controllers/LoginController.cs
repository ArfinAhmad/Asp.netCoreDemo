using ConsumingWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ConsumingWebApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user.UserName.Equals("admin") && user.Password.Equals("Password") )
            {
                user.Sno = new Random().Next();  
                var token = GenerateJwtToken(user);
                return Ok(token);
            }
            return BadRequest("Invalid User");
        }
         
        private string GenerateJwtToken(User user)
        {
            var securitykey = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Sno.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(securitykey), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
