using CandyApi.Extensions;
using CandyApi.Repository.Implementations;
using CandyApi.Repository.Interfaces;
using CandyApi.Services;

var builder = WebApplication.CreateBuilder(args);
// Servicios
builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllersWithValidation();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddScoped<IUserRespository, UserRepository>();
builder.Services.AddScoped<IMaterialesRepository, MaterialesRepository>();
builder.Services.AddSingleton<IJwtService, JwtService>(); 

var app = builder.Build();

// Middleware
app.VerifyDatabaseConnection();
app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();