
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
            
            
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "docs/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    // Usar scheme y host ya procesados por ForwardedHeaders
                  var scheme = httpReq.Scheme;
                    var host = httpReq.Host.Value;
                    var serverUrl = $"{scheme}://{host}{basePath}";
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new() { Url = serverUrl, Description = "API Server" }
                    };
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
