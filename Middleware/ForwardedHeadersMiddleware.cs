

namespace CandyApi.Middleware;

public class ForwardedHeadersMiddleware
{
private readonly RequestDelegate _next;
    private readonly ILogger<ForwardedHeadersMiddleware> _logger;

    public ForwardedHeadersMiddleware(RequestDelegate next, ILogger<ForwardedHeadersMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Detectar si viene de proxy HTTPS
        var forwardedProto = context.Request.Headers["X-Forwarded-Proto"].FirstOrDefault();
        var forwardedHost = context.Request.Headers["X-Forwarded-Host"].FirstOrDefault();
        var originalScheme = context.Request.Scheme;

        if (!string.IsNullOrEmpty(forwardedProto))
        {
            context.Request.Scheme = forwardedProto;
            _logger.LogDebug("Scheme cambiado de {Original} a {Forwarded}", originalScheme, forwardedProto);
        }

        if (!string.IsNullOrEmpty(forwardedHost))
        {
            context.Request.Host = new HostString(forwardedHost);
        }

        // Detectar contenido mixto (proxy HTTPS pero request HTTP)
        if (forwardedProto == "https" && originalScheme == "http")
        {
            _logger.LogInformation("Petición a través de proxy HTTPS detectada");
        }

        await _next(context);
    }
}
