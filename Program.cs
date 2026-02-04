
using System.Text;
using CandyApi.Data;
using CandyApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese 'Bearer {token}'"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] {}
        }
    });
});

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var secretKey = builder.Configuration.GetValue<string>("ApiSetting:SecretKey");
var keyBytes = Encoding.UTF8.GetBytes(secretKey!);



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ClockSkew = TimeSpan.Zero
    };
});



builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("ConexionSql");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IUserRespository, UserRepository>();

var app = builder.Build();

#region Verificar conexión a la base de datos

using (var scope = app.Services.CreateScope())
{
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
            logger.LogError("No se pudo conectar a la base de datos usando la cadena desde appsettings.json."); 
            throw new InvalidOperationException("No se puede conectar a la base de datos.");
        }      
            
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error al verificar la conexión a la base de datos");        
        throw;
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        // Cambia la plantilla de ruta del JSON de Swagger
        c.RouteTemplate = "docs/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        // Indica el endpoint del JSON generado por UseSwagger
        c.SwaggerEndpoint("/docs/v1/swagger.json", "CandyApi V1");
        // Muestra la UI en /docs en lugar de /swagger
        c.RoutePrefix = "docs";
        // Opcional: título de la pestaña
        c.DocumentTitle = "CandyApi - Documentación API";
    });
}
#endregion

app.UseHttpsRedirection();

app.UseCors();
app.MapControllers();



app.Run();

