using Microsoft.OpenApi.Models;

namespace CandyApi.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Ingrese el token JWT"
            });
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API CandyCRM",
                Version = "v1",
                Description = "API CandyCRM para gestionar elementos presupuestados",
                Contact = new OpenApiContact
                {
                    Name = "Soporte API CandyCRM",
                    Email = "mestelles@litoprocess.com",
                },
            });
            c.OperationFilter<AuthorizeCheckOperationFilter>();
        });
        return services;
    }

    public static WebApplication UseSwaggerConfiguration(this WebApplication app, string? basePath = null)
    {
        if (app.Environment.IsDevelopment())
        {
            var forceHttps = app.Configuration.GetValue<bool>("ApiSetting:ForceHttps");
            var internalPort = app.Configuration.GetValue<string>("ApiSetting:InternalPort") ?? "5050";

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "docs/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {

                    var forwardedProto = httpReq.Headers["X-Forwarded-Proto"].FirstOrDefault();
                    var forwardedHost = httpReq.Headers["X-Forwarded-Host"].FirstOrDefault();                    
                    var servers = new List<OpenApiServer>();
                            
                    // Si viene de proxy, agregar también el interno
                    if (!string.IsNullOrEmpty(forwardedHost))
                    {
                        var internalHost = httpReq.Host.Value;
                        var isHttps = !string.IsNullOrEmpty(forwardedProto) 
                        ? forwardedProto.Split(',')[0].Trim().Equals("https", StringComparison.OrdinalIgnoreCase)
                        : httpReq.IsHttps;

                        var clientScheme = isHttps ? "https" : "http";  
                        var clientHost = !string.IsNullOrEmpty(forwardedHost) 
                        ? forwardedHost.Split(',')[0].Trim() 
                        : httpReq.Host.Value;
                      
                        servers.Add(new OpenApiServer 
                        { 
                            Url = $"{clientScheme}://{clientHost}{basePath}", 
                            Description = "🏠 Interno (directo)" 
                        });
                    }
                    
                    swaggerDoc.Servers = servers;
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "CandyApi V1");
                c.RoutePrefix = "docs";
                c.DocumentTitle = "CandyApi - Documentación API";
            });
        }
        return app;
    }
}