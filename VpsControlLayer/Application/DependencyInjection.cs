using Microsoft.Extensions.DependencyInjection;
using VpsControlLayer.Application.Abstractions.Commands;
using VpsControlLayer.Application.Commands;

namespace VpsControlLayer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Регистрация Command Handlers
        services.AddTransient<IInitCommandHandler, InitCommandHandler>();
        services.AddTransient<IDeployCommandHandler, DeployCommandHandler>();
        services.AddTransient<IStatusCommandHandler, StatusCommandHandler>();
        services.AddTransient<IRotateKeysCommandHandler, RotateKeysCommandHandler>();
        
        return services;
    }
}