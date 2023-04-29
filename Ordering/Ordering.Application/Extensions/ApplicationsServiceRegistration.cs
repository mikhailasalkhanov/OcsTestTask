using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Interfaces;

namespace Ordering.Application.Extensions;

public static class ApplicationsServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}