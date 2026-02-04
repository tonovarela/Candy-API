
using Microsoft.AspNetCore.Mvc;
namespace CandyApi.Extensions;

public static class ControllerExtensions
{
     public static IServiceCollection AddControllersWithValidation(this IServiceCollection services)
    {
        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = new List<object>();
                    foreach (var kvp in context.ModelState)
                    {
                        var msgs = new List<string>();
                        if (kvp.Value != null)
                        {
                            foreach (var err in kvp.Value.Errors)
                            {
                                msgs.Add(string.IsNullOrWhiteSpace(err.ErrorMessage)
                                    ? "Valor inválido"
                                    : err.ErrorMessage);
                            }
                        }
                        if (msgs.Count > 0)
                            errors.Add(new { Field = kvp.Key, Messages = msgs.ToArray() });
                    }

                    return new BadRequestObjectResult(new
                    {
                        message = "Error de validación en los datos enviados",
                        details = errors
                    });
                };
            });

        return services;
    }

}
