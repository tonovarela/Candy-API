using CandyApi.Extensions;
using CandyApi.Repository.Implementations;
using CandyApi.Repository.Interfaces;
using CandyApi.Services;
using Microsoft.AspNetCore.HttpOverrides;




var builder = WebApplication.CreateBuilder(args);
// Servicios
builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllersWithValidation();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddScoped<IUserRespository, UserRepository>();
builder.Services.AddScoped<IMaterialesRepository, MaterialesRepository>();
builder.Services.AddSingleton<IJwtService, JwtService>(); 

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor 
                             | ForwardedHeaders.XForwardedProto 
                             | ForwardedHeaders.XForwardedHost;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
    // Permitir cualquier proxy (ajustar según tu red)
    options.KnownNetworks.Add(new IPNetwork(System.Net.IPAddress.Parse("10.0.0.0"), 8));
    options.KnownNetworks.Add(new IPNetwork(System.Net.IPAddress.Parse("172.16.0.0"), 12));
    options.KnownNetworks.Add(new IPNetwork(System.Net.IPAddress.Parse("192.168.0.0"), 16));
});

var app = builder.Build();
var basePath = builder.Configuration.GetValue<string>("ApiSetting:BasePath") ?? "";
Console.WriteLine($"BasePath: {basePath ?? "No base path set"}");

// Middleware

app.VerifyDatabaseConnection();
app.UseSwaggerConfiguration(basePath);
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();