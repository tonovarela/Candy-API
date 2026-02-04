using CandyApi.Constants;
using CandyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CandyApi.Controllers
{
    [Route("api/users")]
    [EnableCors(PolicyNames.AllowSpecificOrigins)]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly IJwtService _jwtService;

        public UsersController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet("")]
        public IActionResult list()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            JwtPayload? payload = null;
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader["Bearer ".Length..].Trim();
                payload = _jwtService.DecodeToken(token);
            }

            return Ok(new
            {
                Message = "Consulta exitosa",                
                Payload = payload
            });
        }
    }
}
