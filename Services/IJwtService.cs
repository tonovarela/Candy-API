using System;
using System.Security.Claims;

namespace CandyApi.Services;

public interface IJwtService
{
 string GenerateToken(string username, string role = "User");
    JwtPayload? DecodeToken(string token);
    ClaimsPrincipal? ValidateToken(string token);
}
