using Microsoft.Extensions.DependencyInjection;
using ProcessDock.Application.Services;

namespace ProcessDock.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IWorkSessionService, WorkSessionService>();

        return services;
    }
}
