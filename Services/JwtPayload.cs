using System;

namespace CandyApi.Services;

public class JwtPayload
{
 public string? Username { get; set; }
    public string? Role { get; set; }
    public DateTime? Expiration { get; set; }
    public Dictionary<string, object> Claims { get; set; } = new();
}
