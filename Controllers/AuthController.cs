using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CandyApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CandyApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRespository _userRepository;
        public AuthController(IConfiguration config, IUserRespository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }


         public record LoginRequest(string Username, string Password);

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest request)
        {
         
            bool isValid= _userRepository.IsValidUserCredentials(request.Username, request.Password);            
            if (isValid == false) 
                return Unauthorized(new { message = "Credenciales inválidas" });

            var secret = _config.GetValue<string>("ApiSetting:SecretKey");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim("role", "User")
            };

            var token = new JwtSecurityToken(
                issuer: "Litoprocess S.A de C.V.", 
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString });
        }

    }
}
