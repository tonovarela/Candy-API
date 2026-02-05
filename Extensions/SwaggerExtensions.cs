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
                Description = "Ingrese el token JWT \n\r\n\r En el formato: Bearer {token} que se obtiene desde el endpoint de autenticación."
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
            // Configurar PathBase si hay proxy
            if (!string.IsNullOrEmpty(basePath))
            {
                app.UsePathBase(basePath);
            }

            // Usar headers del proxy
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor 
                                 | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
                                 | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedHost
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "docs/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    // Ajustar URL base según headers del proxy
                    var serverUrl = $"{httpReq.Scheme}://{httpReq.Host.Value}{basePath}";
                    swaggerDoc.Servers = new List<OpenApiServer> { new() { Url = serverUrl } };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{basePath}/docs/v1/swagger.json", "CandyApi V1");
                c.RoutePrefix = "docs";
                c.DocumentTitle = "CandyApi - Documentación API";
            });
        }
        return app;
    }

}
