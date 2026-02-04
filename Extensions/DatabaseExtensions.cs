using CandyApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CandyApi.Extensions;

public static class DatabaseExtensions
{
     public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("ConexionSql");
        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(connectionString));
        return services;
    }

    public static WebApplication VerifyDatabaseConnection(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        try
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            if (db.Database.CanConnect())
            {
                logger.LogInformation("Conexión a la base de datos: OK");
            }
            else
            {
                logger.LogError("No se pudo conectar a la base de datos.");
                throw new InvalidOperationException("No se puede conectar a la base de datos.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al verificar la conexión a la base de datos");
            throw;
        }
        return app;
    }

}
