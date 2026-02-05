using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CandyApi.Services;

public class JwtService:IJwtService
{
    private readonly string _secretKey;
    private readonly string _issuer;

    public JwtService(IConfiguration config)
    {
        _secretKey = config.GetValue<string>("ApiSetting:SecretKey") 
            ?? throw new InvalidOperationException("SecretKey no configurada");
        _issuer = "Litoprocess S.A de C.V.";
    }

    public Tuple<string, DateTime> GenerateToken(string username, string role = "User")
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim("role", role)
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Tuple.Create(tokenString,token.ValidTo.ToLocalTime());
    }

    public JwtPayload? DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        
        if (!handler.CanReadToken(token))
            return null;

        var jwt = handler.ReadJwtToken(token);
        
        return new JwtPayload
        {
            Username = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value 
                       ?? jwt.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value,
            Role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value 
                   ?? jwt.Claims.FirstOrDefault(c => c.Type == "role")?.Value,
            Expiration = jwt.ValidTo,
            Claims = jwt.Payload.ToDictionary(k => k.Key, v => v.Value)
        };
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var handler = new JwtSecurityTokenHandler();

        try
        {
            var principal = handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            }, out _);

            return principal;
        }
        catch
        {
            return null;
        }
    }

}
