
using System.IdentityModel.Tokens.Jwt;
using CandyApi.Constants;
using CandyApi.Repository;
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

      

        [HttpGet("")]
        public IActionResult list()
        {
            Console.WriteLine("Iniciando sesión...");
            var claims =User.Claims.Select(c => new { c.Type, c.Value }).ToList();            
            string? authHeader = Request.Headers["Authorization"].FirstOrDefault();
            string? token = null;
            object? payload = null;
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                token = authHeader.Substring("Bearer ".Length).Trim();                
                var handler = new JwtSecurityTokenHandler();
                if (handler.CanReadToken(token))
                {
                    var jwt = handler.ReadJwtToken(token);
                    payload = jwt.Payload; // objeto con las claves del payload
                }
            }

            return Ok(new
            {
                Message = "Inicio de sesión exitoso!!!!",
                Claims = claims,                
                Payload = payload
            });
        }
    }
}
