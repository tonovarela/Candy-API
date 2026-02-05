using CandyApi.Extensions;
using CandyApi.Middleware;
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
var basePath = builder.Configuration.GetValue<string>("ApiSetting:BasePath") ?? "";
Console.WriteLine($"BasePath: {basePath ?? "No base path set"}");

// Middleware
app.UseMiddleware<ForwardedHeadersMiddleware>();
app.VerifyDatabaseConnection();
app.UseSwaggerConfiguration(basePath);
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();