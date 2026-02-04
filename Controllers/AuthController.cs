using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CandyApi.DTO;
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




        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {

            bool isValid = await _userRepository.IsValidUserCredentials(loginDTO);
            if (isValid == false)
                return Unauthorized(new { message = "Credenciales inválidas" });
            var tokenString = GenerateJwtToken(loginDTO.Username!);            
            return Ok(new { token = tokenString });
        }

    

    private string GenerateJwtToken(string username)
    {
        var secret = _config.GetValue<string>("ApiSetting:SecretKey");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("role", "User")
        };

        var token = new JwtSecurityToken(
            issuer: "Litoprocess S.A de C.V.",
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
}


