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

    public static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        // if (app.Environment.IsDevelopment())
        // {
            app.UseSwagger(c => c.RouteTemplate = "docs/{documentName}/swagger.json");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/docs/v1/swagger.json", "CandyApi V1");
                c.RoutePrefix = "docs";
                c.DocumentTitle = "CandyApi - Documentación API";
            });
        //}
        return app;
    }

}
