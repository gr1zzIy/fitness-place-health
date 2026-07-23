using FluentValidation;
using GymApp.Application.Gyms;
using Microsoft.Extensions.DependencyInjection;

namespace GymApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<IGymService, GymService>();
        
        return services;
    }
}