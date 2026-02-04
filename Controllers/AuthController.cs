using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CandyApi.DTO;
using CandyApi.Repository;
using CandyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CandyApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
            private readonly IJwtService _jwtService;        
            private readonly IUserRespository _userRepository;

        public AuthController( IUserRespository userRepository,IJwtService jwtService)
        {            
            _userRepository = userRepository;
            _jwtService = jwtService;
        }




        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
        bool isValid = await _userRepository.IsValidUserCredentials(loginDTO);
        if (!isValid)
            return Unauthorized(new { message = "Credenciales inválidas" });

        var token = _jwtService.GenerateToken(loginDTO.Username!);
        return Ok(new { token });
        
        }

    

    
}
}


